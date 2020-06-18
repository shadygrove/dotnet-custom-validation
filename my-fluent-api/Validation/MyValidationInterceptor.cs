using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyValidation.Core.V2;
using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ResponseModels;
using System;

namespace my_fluent_api.Validation
{
    public class MyValidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            var valCtx = validationContext;

            // Note that ValidationResult is a FluentValidation.Results.ValidationResult in this context (not System.Net.DataAnnotations)
            foreach (var error in result.Errors)
            {
                MyFluentCodes errorCode = MyFluentCodes.NotDefined;
                Enum.TryParse<MyFluentCodes>(error.ErrorCode.Replace("Validator", "Error"), out errorCode);

                controllerContext.ModelState.TryAddModelException(error.PropertyName, new MyValidationException(error.ErrorMessage, errorCode));
            }

            return new ValidationResult();
        }

        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }
    }
}
