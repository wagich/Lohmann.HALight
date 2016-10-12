using Lohmann.HALight.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lohmann.HALight.Formatters
{
    /// <summary>
    /// Helper class which provides <see cref="JsonSerializerSettings"/>.
    /// </summary>
    public static class HalJsonSerializerSettingsProvider
    {
        public const string HalMediaType = "application/hal+json";

        private const int DefaultMaxDepth = 32;

        /// <summary>
        /// Creates default <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <returns>Default <see cref="JsonSerializerSettings"/>.</returns>
        public static JsonSerializerSettings CreateDefaultSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),

                NullValueHandling  = NullValueHandling.Ignore,

                DefaultValueHandling = DefaultValueHandling.Ignore,

                MissingMemberHandling = MissingMemberHandling.Ignore,
                
                // Limit the object graph we'll consume to a fixed depth. This prevents stackoverflow exceptions
                // from deserialization errors that might occur from deeply nested objects.
                MaxDepth = DefaultMaxDepth,

                // Do not change this setting
                // Setting this to None prevents Json.NET from loading malicious, unsafe, or security-sensitive types
                TypeNameHandling = TypeNameHandling.None
            };

            return settings;
        }

        public static JsonSerializerSettings AppendHalConverters(JsonSerializerSettings settings)
        {
            settings.Converters.Add(new RelationsConverter());
            settings.Converters.Add(new ResourceConverter());
            return settings;
        }
    }
}