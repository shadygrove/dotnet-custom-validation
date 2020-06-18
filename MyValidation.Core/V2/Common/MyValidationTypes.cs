using System.Text.Json.Serialization;

namespace MyValidation.Core.V2.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MyValidationTypes
    {
        FLUENT_VALIDATION,
        REQUIRED,
        MIN,
        MAX,
        MIN_LENGTH,
        MAX_LENGTH,
        OUT_OF_RANGE,
        MUST_MATCH,
        OTHER 
    }
}
