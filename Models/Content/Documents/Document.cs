﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Documents
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Document : Content
    {
        public string Author { get; set; }
        public List<Guid> Category { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public Guid? FolderId { get; set; }
        public string MimeType { get; set; }
        public Single Ordinal { get; set; }
        public string Parts { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> Tags { get; set; }
        public string ThumbnailUrl { get; set; }
        public long TotalSize { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
