using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyValidation.Core.V2.Common;
using System;

namespace MyValidation.Core.V2.ResponseModels
{
    public class ValidationResponseModel /*: ModelValidationResult*/
    {
        public string Type { get; private set; }

        public string Message { get; private set; }


        public ValidationResponseModel(ValidatorType type, string message)
        {
            this.Type = type.ToString();
            this.Message = message;
        }
    }
}
