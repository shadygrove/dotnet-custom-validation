using MyValidation.Core.V2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyValidation.Core.V2.ValidationResults
{
    public class MyValidationResult : ValidationResult
    {
        public MyValidationTypes ValidationType { get; private set; }

        public MyFluentCodes FluentType { get; private set; }

        public MyValidationResult(MyValidationTypes type) : base(type.ToString())
        {
            this.ValidationType = type;
            this.ErrorMessage = FormatMessage(this.ValidationType, String.Empty);
        }

        public MyValidationResult(MyFluentCodes type) : base(type.ToString())
        {
            this.ValidationType = MyValidationTypes.FLUENT_VALIDATION;
            this.FluentType = type;
            this.ErrorMessage = FormatMessage(this.FluentType, String.Empty);
        }

        public MyValidationResult(MyValidationTypes type, ValidationResult baseResult) : base(baseResult)
        {
            this.ValidationType = type;
            this.ErrorMessage = FormatMessage(this.FluentType, baseResult.ErrorMessage);
        }

        public MyValidationResult(MyFluentCodes type, ValidationResult baseResult) : base(baseResult)
        {
            this.ValidationType = MyValidationTypes.FLUENT_VALIDATION;
            this.FluentType = type;
            this.ErrorMessage = FormatMessage(this.FluentType, baseResult.ErrorMessage);
        }

        //public MyValidationResult(MyValidationTypes type, string message) : base(type.ToString())
        //{
        //    this.ValidationType = type;
        //    this.ErrorMessage = this.FormatMessage(type, message);
        //}

        //public MyValidationResult(MyValidationTypes type, ValidationResult validationResult) : base(validationResult) {
        //    this.ValidationType = type;
        //}

        public MyValidationResult(MyValidationTypes type, IEnumerable<string> memberNames) : base(type.ToString(), memberNames)
        {
            this.ValidationType = type;
            this.ErrorMessage = FormatMessage(this.ValidationType, String.Empty);
        }

        //public MyValidationResult(MyValidationTypes type, IEnumerable<string> memberNames, string message) : base(type.ToString(), memberNames)
        //{
        //    this.ValidationType = type;
        //    this.ErrorMessage = this.FormatMessage(type, message);
        //}

        private string FormatMessage(MyValidationTypes type, string message)
        {
            return "MyValidationResult V2: " + type.ToString() + message;
        }

        private string FormatMessage(MyFluentCodes type, string message)
        {
            return "MyValidationResult V2: " + type.ToString() + message;
        }
    }
}
