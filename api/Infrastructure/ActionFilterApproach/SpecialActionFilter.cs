using api.Infrastructure.ActionFilterApproach;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.ValidationFilterApproach
{

    /*
     *  To make this work you need to disable model validation
     *  options.SuppressModelStateInvalidFilter = true;
     *  
     * */

    public class SpecialActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Find any action arguments that implement ISpecialValidatableObject
            // and call ValidateSpecial()
            // if we get results, then set the context.Result to short-circuit the request pipeline
            foreach (var arg in context.ActionArguments)
            {
                if (arg.Value is IValidatableObject)
                {
                    IEnumerable<ValidationResult> argResults = (arg.Value as IValidatableObject).Validate(null);

                    validationResults.AddRange(argResults);
                }
            }

            if (validationResults.Count > 0)
            {
                SpecialValidationResultEnvelope envelope = new SpecialValidationResultEnvelope("The request was invalid", Guid.NewGuid().ToString());

                var memberNames = validationResults.SelectMany(v => v.MemberNames);

                foreach (string memberName in memberNames)
                {
                    var temp = validationResults.Where(v => v.MemberNames.Contains(memberName))
                                .Select(m => (SpecialValidationResult)m);

                    var valResult = new SpecialFieldValidationResult();
                    valResult.FieldName = memberName;
                    valResult.Errors = temp;

                    envelope.Errors.Add(valResult);
                }
                envelope.ErrorsV2 = validationResults.Select(vr => (SpecialValidationResult)vr);

                context.Result = new BadRequestObjectResult(envelope);
            }
        }
    }
}
