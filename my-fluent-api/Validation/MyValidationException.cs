using MyValidation.Core.V1.ResponseModels;
using System.ComponentModel.DataAnnotations;

namespace my_fluent_api.Validation
{
    public class MyValidationException : ValidationException
    {
        public ErrorCode ErrorCode { get; set; }

        public MyValidationException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
