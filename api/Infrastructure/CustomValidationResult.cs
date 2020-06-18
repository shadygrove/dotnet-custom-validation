using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure
{
    public class CustomValidationResult : ValidationResult
    {
        public string CustomMessage { get; }

        public CustomValidationResult(string message): base("CustomValidationResult: " + message) {
            this.CustomMessage = "My Custom validation message";
        }

        public CustomValidationResult(string message, IEnumerable<string> memberNames) : base("CustomValidationResult: " + message, memberNames)
        {
            this.CustomMessage = "My Custom validation message with member names";
        }
    }
}
