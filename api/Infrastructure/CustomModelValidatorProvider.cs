using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure
{
    public class CustomModelValidatorProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            context.Results.Add(new CustomValidatorItem() { Validator = new CustomValidator() });

            //throw new NotImplementedException();
        }

        public class CustomValidatorItem : ValidatorItem
        {
            public string MyValidationCode { get; set; }

            public CustomValidatorItem () {
                this.MyValidationCode = "YubbaDubbaDooba";
            }
        }
    }
}
