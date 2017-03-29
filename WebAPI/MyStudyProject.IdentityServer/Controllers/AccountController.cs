﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using MyStudyProject.IdentityServer.Identity;
using MyStudyProject.IdentityServer.Infrastructure;
using MyStudyProject.IdentityServer.Services;
using MyStudyProject.IdentityServer.ViewModels;

namespace MyStudyProject.IdentityServer.Controllers
{
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private IAccountService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountService service)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.service = service;
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            returnUrl = Url.Action("ExternalLoginCallback", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
            return Challenge(properties, provider);
        }

        public async Task<LoginViewModel> Login(string returnUrl)
        {
            LoginViewModel vm = await service.BuildLoginViewModelAsync(returnUrl);
            return vm;
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            bool result = false;
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info != null)
            {
                var tempUser = info.Principal;
                var claims = tempUser.Claims.ToList();
   
                var userIdClaim = claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                var email = "EvilAvenger@yandex.ru";
                //claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email);

                if (userIdClaim != null)
                {
                    var isRegistered = await IsUserRegistered(info.LoginProvider, info.ProviderKey);
                    if (!isRegistered && email != null)
                    {
                        var user = new ApplicationUser { UserName = userIdClaim.Value, Email = email };
                        var userCreated = await userManager.CreateAsync(user);
                        isRegistered = userCreated.Succeeded;

                        if (isRegistered)
                        {
                            var addLoginresult = await userManager.AddLoginAsync(user, info);
                            isRegistered = addLoginresult.Succeeded;
                            if (isRegistered)
                            {
                                await signInManager.SignInAsync(user, isPersistent: false);
                            }
                        }
                    }

                    if (isRegistered)
                    {
                        var succeded = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
                        if (succeded.Succeeded)
                        {
                            IdentityResult updateResult = await signInManager.UpdateExternalAuthenticationTokensAsync(info);
                            result = updateResult.Succeeded;


                            var key = "O7OYOgmutGRemGCThi51DYgyL";
                            var secretKey = "496fR6J70pryWgsKLYTOGvwpmKpYmmfJGm84bpmwmt4e866zRC";
                            var access = info.AuthenticationTokens.ToList().First(x=>x.Name == "access_token").Value;
                            var secret = info.AuthenticationTokens.ToList().First(x => x.Name == "access_token_secret").Value;

                            var twi = new TwitterLoginVerifier();
                            var rest = await twi.TwitterLoginAsync(access, secret, key, secretKey);
                            //AuthFlow.CreateCredentialsFromVerifierCode();
                        }
                    }
                }
            }

            if (!result)
            {
                await signInManager.SignOutAsync();
            }
            return Redirect(returnUrl);
        }

        private async Task<bool> IsUserRegistered(string login, string key)
        {
            return await userManager.FindByLoginAsync(login, key) != null;
        }
    }
}
