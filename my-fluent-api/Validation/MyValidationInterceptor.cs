using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using my_fluent_api.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_fluent_api.Validation
{
    public class MyValidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            var valCtx = validationContext;

            foreach (var error in result.Errors)
            {
                ErrorCode ecode;
                Enum.TryParse<ErrorCode>(error.ErrorCode, out ecode);
                controllerContext.ModelState.TryAddModelException(error.PropertyName, new MyValidationException(error.ErrorMessage, ecode));
            }

            return new ValidationResult();
        }

        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }
    }
}
