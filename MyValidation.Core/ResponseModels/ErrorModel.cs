using System.Text.Json.Serialization;

namespace MyValidation.Core.ResponseModels
{
    public class ErrorModel
    {
        /// <summary>
        /// The field name thats causing a validation error
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// The error code
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorCode ErrorCode { get; set; }
        /// <summary>
        /// the error message
        /// </summary>
        public string Message { get; set; }
    }
}
