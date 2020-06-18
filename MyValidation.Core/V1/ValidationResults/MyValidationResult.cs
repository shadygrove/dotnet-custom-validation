using MyValidation.Core.V1.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyValidation.Core.V1.ValidationResults
{
    public class XMyValidationResult : ValidationResult
    {
        public ErrorCode ErrorCode { get; set; }

        public XMyValidationResult(string errorMessage) : base(errorMessage)
        {
            this.ErrorMessage = "MyValidationResult: " + errorMessage;
        }

        public XMyValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
            this.ErrorMessage = "MyValidationResult: " + errorMessage;
        }

        public XMyValidationResult(System.ComponentModel.DataAnnotations.ValidationResult validationResult) : base(validationResult)
        {
            this.ErrorMessage = "MyValidationResult: " + validationResult.ErrorMessage;
        }

        protected XMyValidationResult(XMyValidationResult validationResult) : base(validationResult) { }
    }
}
