using Microsoft.AspNetCore.Mvc;
using MyValidation.Core.V1.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation
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


            var errorResponse = new ErrorResponse();

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {

                    var errorModel = new ErrorModel();
                    errorModel.Message = subError.ErrorMessage;
                    errorModel.FieldName = error.Key;

                    if (subError.Exception is MyValidationException)
                    {
                        var exception = (MyValidationException)subError.Exception;
                        errorModel.ErrorCode = exception.ErrorCode;
                        errorModel.Message = exception.Message;
                    }

                    errorResponse.Errors.Add(errorModel);
                }
            }
            return new BadRequestObjectResult(errorResponse);
        }
    }
}
