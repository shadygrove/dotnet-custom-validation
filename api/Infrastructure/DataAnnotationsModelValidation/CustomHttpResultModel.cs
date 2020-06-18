using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.DataAnnotationsModelValidation
{
    public class CustomHttpResultModel
    {
        public string Message { get; set; }

        public List<ValidationError> Errors { get; set; }

        public CustomHttpResultModel()
        {
        }
    }
}
