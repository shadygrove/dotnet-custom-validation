using my_fluent_api.Validation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
