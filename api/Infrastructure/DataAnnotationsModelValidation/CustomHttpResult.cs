using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.DataAnnotationsModelValidation
{
    public class CustomHttpResult : ObjectResult
    {
        public CustomHttpResult(CustomHttpResultModel model) 
            : base(model)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
