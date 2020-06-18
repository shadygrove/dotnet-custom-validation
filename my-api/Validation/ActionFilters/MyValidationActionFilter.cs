﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using my_api.Validation.Models;
using MyValidation.Core.V2.ResponseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace my_api.Validation.ActionFilters
{
    public class MyValidationActionFilter : ActionFilterAttribute
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
                MyValidationResponseEnvelope envelope = new MyValidationResponseEnvelope();
                envelope.Message = "Bad Request: There were validation errors";

                var memberNames = validationResults.SelectMany(v => v.MemberNames);

                foreach (MyValidationResult valResult in validationResults)
                {
                    foreach (string memberName in valResult.MemberNames)
                    {
                        var valModel = new MyValidationResponseModel(valResult.ValidationType, memberName);

                        envelope.Errors.Add(valModel);
                    }
                }

                context.Result = new BadRequestObjectResult(envelope);
            }
        }
    }
}
