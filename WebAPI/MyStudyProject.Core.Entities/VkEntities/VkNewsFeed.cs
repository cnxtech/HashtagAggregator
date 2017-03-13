﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyStudyProject.Core.Entities.VkEntities
{
    public class VkNewsFeed
    {
        [JsonProperty("items")]
        public List<VkNewsSearchResult> Feed { get; set; }

        [JsonProperty("profiles")]
        public List<VkNewsProfile> Profiles { get; set; }
    }
}