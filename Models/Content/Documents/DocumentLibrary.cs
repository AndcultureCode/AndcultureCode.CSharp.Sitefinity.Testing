﻿using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Documents
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DocumentLibrary : Content
    {
        public string BlobStorageProvider { get; set; }
        public int ChildrenCount { get; set; }
        public string ClientCacheProfile { get; set; }
        public Guid CoverId { get; set; }
        public string Description { get; set; }
        public long MaxSize { get; set; }
        public long MaxItemSize { get; set; }
        public string OutputCacheProfile { get; set; }
        public string Title { get; set; }
    }
}
