using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.ActionFilterApproach
{
    public class SpecialValidationResult : ValidationResult
    {
        public string FieldName { get; set; }
        public string ErrorCode { get; set; }
        
        public string Message { get; set; }
        
        public SpecialValidationResult(string message, string errorCode, string[] memberNames) : base(message, memberNames) {
            this.ErrorCode = errorCode ?? "DEFAULT";
        }

        //public SpecialValidationResult(string fieldName, string errorCode, string message) : base(message)
        //{
        //    this.FieldName = fieldName;
        //    this.ErrorCode = errorCode;
        //    this.Message = message;
        //}
    }
}
