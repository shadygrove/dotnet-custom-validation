using FluentValidation;
using MyValidation.Core.V1.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyValidation.Core.V1
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
