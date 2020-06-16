using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using my_api.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Validation.ResponseModels
{
    public class MyValidationResponseModel : ModelValidationResult
    {
        public MyValidationTypes Type { get; private set; }

        public MyValidationResponseModel(MyValidationTypes type, string memberName, string message): base(memberName, message) {
            this.Type = type;
        }
    }
}
