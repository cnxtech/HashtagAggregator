﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyStudyProject.IdentityServer.Infrastructure
{
    public interface ITwitterVerifier
    {
        Task<string> GetEmailAsync(ExternalLoginInfo info);
    }
}
