using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.DataAnnotationsModelValidation
{
    public class CustomModelValidationResult : ModelValidationResult
    {
        public string MoreInfo { get; set; }

        public CustomModelValidationResult(string memberName, string message): base(memberName, message)
        {
            this.MoreInfo = "More info is needed";
        }
    }
}
