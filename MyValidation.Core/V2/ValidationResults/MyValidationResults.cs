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
            this.ErrorMessage = FormatMessage(this.ValidationType);
        }

        public MyValidationResult(MyFluentCodes type) : base(type.ToString())
        {
            this.FluentType = type;
            this.ErrorMessage = FormatMessage(this.FluentType);
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
            this.ErrorMessage = FormatMessage(this.ValidationType);
        }

        public MyValidationResult(MyFluentCodes type, IEnumerable<string> memberNames) : base(type.ToString(), memberNames)
        {
            this.FluentType = type;
            this.ErrorMessage = FormatMessage(this.FluentType);
        }

        //public MyValidationResult(MyValidationTypes type, IEnumerable<string> memberNames, string message) : base(type.ToString(), memberNames)
        //{
        //    this.ValidationType = type;
        //    this.ErrorMessage = this.FormatMessage(type, message);
        //}

        private string FormatMessage(MyValidationTypes type)
        {
            return "MyValidationResult V2: " + type.ToString();
        }

        private string FormatMessage(MyFluentCodes type)
        {
            return "MyValidationResult V2: " + type.ToString();
        }
    }
}
