using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;

namespace my_fluent_provider.Validation
{
    public class FluentValidation : IModelValidator
    {
        IValidator _abstractValidator;
        public FluentValidation(IValidator abstractValidator)
        {
            _abstractValidator = abstractValidator;
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            List<ModelValidationResult> modelValidationResults = new List<ModelValidationResult>();

            ValidationResult fluentResult = _abstractValidator.Validate(context.Container);

            foreach (var error in fluentResult.Errors)
            {
                modelValidationResults.Add(
                new ModelValidationResult(error.PropertyName, error.ErrorMessage));
            }

            return modelValidationResults;
        }
    }
}
