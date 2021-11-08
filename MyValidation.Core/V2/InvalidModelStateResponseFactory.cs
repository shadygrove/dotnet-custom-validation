using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ResponseModels;
using System.Collections.Generic;
using System.Linq;

namespace MyValidation.Core.V2
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceResonse(ActionContext actionContext)
        {
            var errorResponse = new ValidationResponseEnvelope();
            errorResponse.Message = "Bad Request: There were validation errors";

            var errorsInModelState = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(field =>
                {
                    List<ValidationResponseModel> errorModels = MapValidationErrors(field.Value.Errors);

                    var result = new KeyValuePair<string, IEnumerable<ValidationResponseModel>>(field.Key, errorModels);
                    return result;
                });

            foreach (var valRs in errorsInModelState)
            {
                errorResponse.Errors.Add(valRs.Key, valRs.Value);
            }
                
            return new BadRequestObjectResult(errorResponse);
        }

        private static List<ValidationResponseModel> MapValidationErrors(ModelErrorCollection modelErrors)
        {
            List<ValidationResponseModel> errorModels = new List<ValidationResponseModel>();
            foreach (var subError in modelErrors)
            {
                ValidationResponseModel valResponse;
                // if we have a custom validation exception use the rich data it provides
                if (subError.Exception is MyValidationException ex)
                {
                    valResponse = new ValidationResponseModel(ex.ValidationType, ex.Message);
                }
                else
                {
                    valResponse = new ValidationResponseModel(ValidatorType.Unknown, subError.ErrorMessage);
                }

                if (valResponse != null)
                {
                    errorModels.Add(valResponse);
                }
            }

            return errorModels;
        }
    }
}
