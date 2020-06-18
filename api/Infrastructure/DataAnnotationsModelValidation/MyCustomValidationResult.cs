using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.DataAnnotationsModelValidation
{
    public class MyCustomValidationResult : ValidationResult
    {
        public string MyProperty { get; set; }

        public MyCustomValidationResult(string errorMessage) : base(errorMessage) {
            this.MyProperty = "My Special Property info here";
        }

        public MyCustomValidationResult(string errorMessage, string[] fields) : base(errorMessage, fields) { }
    }
}
