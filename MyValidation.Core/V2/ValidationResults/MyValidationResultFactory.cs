using MyValidation.Core.V2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyValidation.Core.V2.ValidationResults
{
    public class MyValidationResultFactory
    {
        public static MyValidationResult Create(MyValidatorType type, string message, string memberName)
        {
            return new MyValidationResult(new ValidatorType(type), message, new string[] { memberName });
        }

        public static MyValidationResult Create(MyValidatorType type, string message, IEnumerable<string> memberNames)
        {
            return new MyValidationResult(new ValidatorType(type), message, memberNames);
        }

        public static MyValidationResult Create(MyValidatorType type, ValidationResult baseResult)
        {
            return new MyValidationResult(new ValidatorType(type), baseResult);
        }

        public static MyValidationResult Create(FluentValidatorType type, ValidationResult baseResult)
        {
            return new MyValidationResult(new ValidatorType(type), baseResult);
        }
        

    }
}
