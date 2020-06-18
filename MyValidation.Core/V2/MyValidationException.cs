using FluentValidation;
using MyValidation.Core.V2.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyValidation.Core.V2
{
    public class MyValidationException : ValidationException
    {
        public ValidatorType ValidationType { get; }

        public MyValidationException(string message, ValidatorType validationType) : base(message)
        {
            ValidationType = validationType;
        }

        public MyValidationException(string message, FluentValidatorType validationType) :
            this(message, new ValidatorType(validationType))
        {
        }

        public MyValidationException(string message, MyValidatorType validationType) :
            this(message, new ValidatorType(validationType))
        {
        }
    }
}
