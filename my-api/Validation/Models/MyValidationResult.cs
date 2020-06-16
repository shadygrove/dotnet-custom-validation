using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Validation.Models
{
    public class MyValidationResult : ValidationResult
    {
        public MyValidationTypes ValidationType { get; private set; }

        public MyValidationResult(MyValidationTypes type) : base(type.ToString()) { }

        public MyValidationResult(MyValidationTypes type, ValidationResult validationResult) : base(validationResult) {
            this.ValidationType = type;
        }

        public MyValidationResult(MyValidationTypes type, IEnumerable<string> memberNames) : base(type.ToString(), memberNames) { 
        }
    }
}
