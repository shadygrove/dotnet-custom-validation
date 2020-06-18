using MyValidation.Core.V2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyValidation.Core.V2.ValidationResults
{
    public class MyValidationResultFactory
    {
        public static MyValidationResult Create(MyValidationTypes type, string memberName)
        {
            return new MyValidationResult(type, new string[] { memberName });
        }

        public static MyValidationResult Create(MyValidationTypes type, string memberName, string message)
        {
            return new MyValidationResult(type, new string[] { memberName });
        }

        public static MyValidationResult Create(MyValidationTypes type, IEnumerable<string> memberNames)
        {
            return new MyValidationResult(type, memberNames);
        }

        public static MyValidationResult Create(MyValidationTypes type, ValidationResult baseResult)
        {
            return new MyValidationResult(type, baseResult);
        }

        public static MyValidationResult Create(MyFluentCodes type, ValidationResult baseResult)
        {
            return new MyValidationResult(type, baseResult);
        }
        

    }
}
