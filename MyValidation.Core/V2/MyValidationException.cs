using FluentValidation;
using MyValidation.Core.V2.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyValidation.Core.V2
{
    public class MyValidationException : ValidationException
    {
        public MyFluentCodes FluentCode { get; set; }

        public MyValidationTypes ValidationType { get; set; }

        public MyValidationException(string message, MyFluentCodes fluentCode) : base(message)
        {
            FluentCode = fluentCode;
            ValidationType = MyValidationTypes.FLUENT_VALIDATION;
        }

        public MyValidationException(string message, MyValidationTypes type) : base(message)
        {
            ValidationType = type;
        }
    }
}
