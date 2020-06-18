using MyValidation.Core.V2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyValidation.Core.V2.ValidationResults
{
    public class MyValidationResult : ValidationResult
    {
        public ValidatorType ValidationType { get; private set; }

        public MyValidationResult(ValidatorType type, string message) :
            base(errorMessage: $"MyValidationResult V2: {type} {message}")
        {
            ValidationType = type;
        }
        
        public MyValidationResult(ValidatorType type, ValidationResult baseResult) : 
            base(baseResult)
        {
            base.ErrorMessage = $"MyValidationResult V2: {type} {baseResult.ErrorMessage}";
            ValidationType = type;
        }

        public MyValidationResult(ValidatorType type, string message, IEnumerable<string> memberNames):
            base(errorMessage: $"MyValidationResult V2: {type} {message}", memberNames: memberNames)
        {
            ValidationType = type;
        }


    }
}
