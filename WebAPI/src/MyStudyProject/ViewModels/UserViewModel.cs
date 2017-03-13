﻿using System;
using MyStudyProject.Shared.Contracts.Enums;
using Newtonsoft.Json;

namespace MyStudyProject.ViewModels
{
    public class UserViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("networkId")]
        public string NetworkId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("profileId")]
        public string ProfileId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}