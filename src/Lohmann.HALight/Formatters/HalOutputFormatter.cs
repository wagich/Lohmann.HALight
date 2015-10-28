﻿using System;
using System.Reflection;
using Lohmann.HALight.Formatters.Internal;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Lohmann.HALight.Formatters
{
    public class HalOutputFormatter : JsonOutputFormatter
    {
        public HalOutputFormatter()
            :this(HalJsonSerializerSettingsProvider.CreateSerializerSettings())
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(HalJsonSerializerSettingsProvider.HalMediaType));
        }

        public HalOutputFormatter(JsonSerializerSettings serializerSettings)
            : base(HalJsonSerializerSettingsProvider.AppendHalConverters(serializerSettings))
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(HalJsonSerializerSettingsProvider.HalMediaType));
        }

        protected override bool CanWriteType(Type declaredType, Type runtimeType)
        {
            // TODO, CanWriteType(Type declaredType, Type runtimeType) => Bug in Beta-8?
            return typeof(IResource).GetTypeInfo().IsAssignableFrom(runtimeType.GetTypeInfo());
        }        
    }
}
