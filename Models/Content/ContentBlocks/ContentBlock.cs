﻿using Newtonsoft.Json;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.ContentBlocks
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ContentBlock : Content
    {
        public string Content { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
