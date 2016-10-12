using System;
using System.Buffers;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Lohmann.HALight.Formatters
{
    public class HalOutputFormatter : JsonOutputFormatter
    {
        public HalOutputFormatter()
            : this(HalJsonSerializerSettingsProvider.CreateDefaultSerializerSettings(), ArrayPool<char>.Shared)
        {
        }

        public HalOutputFormatter(JsonSerializerSettings serializerSettings)
            : this(serializerSettings, ArrayPool<char>.Shared)
        {
        }

        public HalOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool)
            : base(HalJsonSerializerSettingsProvider.AppendHalConverters(serializerSettings), charPool)
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(HalJsonSerializerSettingsProvider.HalMediaType));
        }        

        protected override bool CanWriteType(Type type)
        {
            return typeof(IResource).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }        
    }
}
