using Microsoft.AspNetCore.Mvc;
using MyValidation.Core.V2.ResponseModels;
using System.Linq;

namespace MyValidation.Core.V2
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceResonse(ActionContext actionContext)
        {
            var errorsInModelState = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(x => new { x.ErrorMessage, x.Exception }))
                .ToArray();

            // TODO: Change to V2 model
            var errorResponse = new ValidationResponseEnvelope();
            errorResponse.Message = "Bad Request: There were validation errors";

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {

                    //var errorModel = new ValidationResponseModel();
                    //errorModel.Message = subError.ErrorMessage;
                    //errorModel.FieldName = error.Key;

                    ValidationResponseModel valResponse;
                    // if we have a custom validation exception use the rich data it provides
                    if (subError.Exception is MyValidationException ex)
                    {
                        valResponse = new ValidationResponseModel(ex.ValidationType, ex.FluentCode, error.Key, ex.Message);
                    } else
                    {
                        valResponse = new ValidationResponseModel(Common.MyValidationTypes.OTHER, error.Key, subError.ErrorMessage);
                    }

                    errorResponse.Errors.Add(valResponse);
                }
            }
            return new BadRequestObjectResult(errorResponse);
        }
    }
}
