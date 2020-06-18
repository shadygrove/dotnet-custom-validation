using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.DataAnnotationsModelValidation
{
    public class CustomValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                CustomHttpResultModel model = new CustomHttpResultModel();
                model.Message = "Validation Failed";
                model.Errors = context.ModelState.Keys
                        .SelectMany(key => context.ModelState[key].Errors.Select(x => {
                            var test = x is CustomModelValidationResult;
                            return new ValidationError(key, x.ErrorMessage);
                        }))
                        .ToList();

                context.Result = new CustomHttpResult(model);
            }
        }
    }
}
