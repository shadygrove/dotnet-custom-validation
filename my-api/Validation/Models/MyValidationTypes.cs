using System.Text.Json.Serialization;

namespace my_api.Validation.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MyValidationTypes
    {
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
