using Microsoft.AspNetCore.Mvc;
using my_fluent_provider.Validation.ResonseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_fluent_provider.Validation
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceResonse(ActionContext actionContext)
        {

            //var errorsInModelState = actionContext.ModelState.GetExtendedErrors<ErrorCode>();

            var errorsInModelState = actionContext.ModelState.Where(
                    x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(
                        x => {

                            if (x.Exception != null && x.Exception is MyValidationException)
                            {
                                var extendedException = (MyValidationException)x.Exception;

                                return new ExtendedError
                                {
                                    ErrorMessage = x.ErrorMessage,
                                    ErrorCode = extendedException.ErrorCode
                                };
                            }

                            return new ExtendedError
                            {
                                ErrorMessage = x.ErrorMessage,
                            };
                        })).ToArray();


            //Our custom error ErrorResponse model
            var errorResponse = new ErrorResponse();

            foreach (var keyValuePair in errorsInModelState)
            {

                foreach (ExtendedError modelError in keyValuePair.Value)
                {
                    //Our custom error Error model
                    var errorModel = new ErrorModel();

                    errorModel.Message = modelError.ErrorMessage;
                    errorModel.FieldName = keyValuePair.Key;
                    errorModel.ErrorCode = modelError.ErrorCode;

                    errorResponse.Errors.Add(errorModel);
                }
            }
            return new BadRequestObjectResult(errorResponse);
        }
    }
}
