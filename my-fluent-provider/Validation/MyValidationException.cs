using my_fluent_provider.Validation.ResonseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_fluent_provider.Validation
{
    public class MyValidationException : ValidationException
    {
        public ErrorCode ErrorCode { get; set; }
        public MyValidationException(string message, ErrorCode errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
