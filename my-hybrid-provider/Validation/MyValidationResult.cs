using MyValidation.Core.V1.ResponseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_hybrid_provider.Validation
{
    public class MyValidationResult : ValidationResult
    {
        public ErrorCode ErrorCode { get; set; }

        public MyValidationResult(string errorMessage) : base(errorMessage) {
            this.ErrorMessage = "MyValidationResult: " + errorMessage;
        }

        public MyValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) {
            this.ErrorMessage = "MyValidationResult: " + errorMessage;
        }

        public MyValidationResult(System.ComponentModel.DataAnnotations.ValidationResult validationResult) : base(validationResult) {
            this.ErrorMessage = "MyValidationResult: " + validationResult.ErrorMessage;
        }

        protected MyValidationResult(MyValidationResult validationResult) : base(validationResult) { }
    }
}
