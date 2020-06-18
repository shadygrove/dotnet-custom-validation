
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyValidation.Core.V2.Common;
using System;

namespace MyValidation.Core.V2.ResponseModels
{
    public class ValidationResponseModel : ModelValidationResult
    {
        //public MyValidationTypes Type { get; private set; }

        //public MyFluentCodes FluentType { get; private set; }

        public string Type { get; private set; }

        public string Message { get; private set; }

        //public string FieldName { get; private set; }


        public ValidationResponseModel(MyValidationTypes type, string memberName, string message) : base(memberName, type.ToString()) {
            this.Type = type.ToString();
            this.Message = message;
            //this.FieldName = memberName;
        }

        public ValidationResponseModel(MyValidationTypes type, MyFluentCodes fluentType, string memberName, string message) : base(memberName, fluentType.ToString())
        {
            if (type == MyValidationTypes.FLUENT_VALIDATION)
            {
                this.Type = type.ToString() + "-" + fluentType.ToString();
            } 
            else
            {
                this.Type = type.ToString();
            }
            
            this.Message = message;
            //this.FieldName = memberName;
        }

        //private string GetValidationMessage (MyValidationTypes type)
        //{
        //    string prefix = type.ToString() + ": ";
        //    string result = String.Empty;

        //    switch (type)
        //    {
        //        case MyValidationTypes.MAX:
        //            result = "Max value exceeded";
        //            break;
        //        case MyValidationTypes.MAX_LENGTH:
        //            result = "Max length exceeded";
        //            break;
        //        case MyValidationTypes.MIN:
        //            result = "Min value not met";
        //            break;
        //        case MyValidationTypes.MIN_LENGTH:
        //            result = "Min length not met";
        //            break;
        //        case MyValidationTypes.MUST_MATCH:
        //            result = "Does not match";
        //            break;
        //        case MyValidationTypes.OTHER:
        //            result = "Validation error";
        //            break;
        //        case MyValidationTypes.OUT_OF_RANGE:
        //            result = "Value out of range";
        //            break;
        //        case MyValidationTypes.REQUIRED:
        //            result = "Field is required";
        //            break;
        //    }
        //    return prefix + result;
        //}
    }
}
