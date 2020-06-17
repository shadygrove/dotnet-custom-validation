using my_hybrid_provider.Validation.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation
{
    public class MyValidationResult : System.ComponentModel.DataAnnotations.ValidationResult
    {
        public ErrorCode ErrorCode { get; set; }
        public MyValidationResult(string errorMessage) : base(errorMessage) { }
        public MyValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
        public MyValidationResult(System.ComponentModel.DataAnnotations.ValidationResult validationResult) : base(validationResult) { }
        protected MyValidationResult(MyValidationResult validationResult) : base(validationResult) { }
    }
}
