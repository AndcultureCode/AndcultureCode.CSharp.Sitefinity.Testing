﻿using AndcultureCode.CSharp.Sitefinity.Testing.Enumerations;
using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.JsonSerialization.Converters
{
    /// <summary>
    /// Responsible for converting PageType enums values into numeric values
    /// </summary>
    public class PageTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long convertedValue = ((PageType)value).GetHashCode();

            writer.WriteValue(convertedValue.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }
            return Enum.Parse(typeof(PageType), reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PageType);
        }
    }
}
