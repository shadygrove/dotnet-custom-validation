using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyValidation.Core.V1;
using MyValidation.Core.V1.ResponseModels;
using System;
using System.Collections.Generic;

namespace my_hybrid_provider.Validation
{
    public class MyFluentModelValidator : IModelValidator
    {
        IValidator _abstractValidator;
        public MyFluentModelValidator(IValidator abstractValidator)
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
                Enum.TryParse(error.ErrorCode.Replace("Validator", "Error"), out errorCode);

                context.ActionContext.ModelState.TryAddModelException(error.PropertyName, new MyValidationException(error.ErrorMessage, errorCode));
            }

            return modelValidationResults;
        }
    }
}
