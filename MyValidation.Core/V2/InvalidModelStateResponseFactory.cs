using Microsoft.AspNetCore.Mvc;
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
            //var errorsInModelState = actionContext.ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .ToDictionary(
            //        kvp => kvp.Key,
            //        kvp => kvp.Value.Errors.Select(x => new { x.ErrorMessage, x.Exception }))
            //    .ToArray();

            var errorResponse = new ValidationResponseEnvelope();
            errorResponse.Message = "Bad Request: There were validation errors";

            var errorsInModelState = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(field =>
                {
                    List<ValidationResponseModel> errorModels = new List<ValidationResponseModel>();
                    foreach (var subError in field.Value.Errors)
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

                    var result = new KeyValuePair<string, IEnumerable<ValidationResponseModel>>(field.Key, errorModels);
                    return result;
                });

            foreach (var valRs in errorsInModelState)
            {
                errorResponse.Errors.Add(valRs.Key, valRs.Value);
            }
                
            //    //.ToDictionary(
            //    //    kvp => kvp.Key,
            //    //    kvp => kvp.Value.Errors.Select(x => new { x.ErrorMessage, x.Exception }))
            //    //.ToArray();

            //var errorResponse = new ValidationResponseEnvelope();
            //errorResponse.Message = "Bad Request: There were validation errors";

            //foreach (var kv in errorsInModelState)
            //{
            //    List<ValidationResponseModel> validationResponses = new List<ValidationResponseModel>();
            //    foreach (var subError in kv.Value)
            //    {
            //        ValidationResponseModel valResponse;
                    
            //        // if we have a custom validation exception use the rich data it provides
            //        if (subError.Exception is MyValidationException ex)
            //        {
            //            valResponse = new ValidationResponseModel(ex.ValidationType, kv.Key, ex.Message);
            //        } else
            //        {
            //            valResponse = new ValidationResponseModel(ValidatorType.Unknown, kv.Key, subError.ErrorMessage);
            //        }

            //        validationResponses.Add(valResponse);
            //    }

            //    errorResponse.Errors.Add(kv.Key, validationResponses);
            //}
            return new BadRequestObjectResult(errorResponse);
        }
    }
}
