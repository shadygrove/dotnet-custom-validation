using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyValidation.Core.V1.ResponseModels;
using System;

namespace my_fluent_api.Validation
{
    public class MyValidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            var valCtx = validationContext;

            foreach (var error in result.Errors)
            {
                ErrorCode errorCode = ErrorCode.NotDefined;
                Enum.TryParse<ErrorCode>(error.ErrorCode.Replace("Validator", "Error"), out errorCode);

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
