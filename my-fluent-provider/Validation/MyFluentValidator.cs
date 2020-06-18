using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using my_fluent_provider.Validation.ResonseModels;
using System;
using System.Collections.Generic;

namespace my_fluent_provider.Validation
{
    public class MyFluentValidator : IModelValidator
    {
        IValidator _abstractValidator;
        public MyFluentValidator(IValidator abstractValidator)
        {
            _abstractValidator = abstractValidator;
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            List<ModelValidationResult> modelValidationResults = new List<ModelValidationResult>();

            ValidationResult fluentResult = _abstractValidator.Validate(context.Model);

            foreach (var error in fluentResult.Errors)
            {
                ErrorCode errorCode = ErrorCode.NotDefined;
                Enum.TryParse<ErrorCode>(error.ErrorCode.Replace("Validator", "Error"), out errorCode);

                context.ActionContext.ModelState.TryAddModelException(error.PropertyName, new MyValidationException(error.ErrorMessage, errorCode));
            }

            return modelValidationResults;
        }
    }
}
