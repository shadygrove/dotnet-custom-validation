using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.ActionFilterApproach
{
    public class SpecialFieldValidationResult
    {
        public string FieldName { get; set; }

        public IEnumerable<SpecialValidationResult> Errors { get; set; }
    }

    //public class SpecialModelValidationResult : ModelValidationResult
    //{
    //    public string ValidationCode{ get; set; }

    //    public SpecialModelValidationResult(string memberName, string message, string validationCode) : base(memberName, message) {
    //        this.ValidationCode = validationCode;
    //    }
    //}
}
