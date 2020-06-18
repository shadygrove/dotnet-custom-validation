using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyValidation.Core.V2;
using MyValidation.Core.V2.ValidationResults;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_hybrid_provider.Validation
{
    public class MvcModelValidator : IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            List<ModelValidationResult> modelValidationResults = new List<ModelValidationResult>();

            var validationResults = new List<ValidationResult>();

            ValidationContext validationcontext = new ValidationContext(context.Model);

            bool isValid = Validator.TryValidateObject(context.Model, validationcontext, validationResults, true);

            foreach (var result in validationResults)
            {
                if (result is MyValidationResult)
                {
                    MyValidationResult myValidationResult = (MyValidationResult)result;
                    foreach (var membername in result.MemberNames)
                    {
                        context.ActionContext.ModelState.TryAddModelException(membername, new MyValidationException(result.ErrorMessage, myValidationResult.ValidationType));
                    }
                }
                else
                {
                    foreach (var membername in result.MemberNames)
                    {
                        modelValidationResults.Add(new ModelValidationResult(membername, result.ErrorMessage));
                    }
                }
            }

            return modelValidationResults;
        }
    }
}
