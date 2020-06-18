
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyValidation.Core.V2.ResponseModels
{
    public class ValidationResponseEnvelope
    {
        public string Message { get; set; }

        public HttpStatusCode Code { get; private set; }

        public List<ValidationResponseModel> Errors { get; set; } = new List<ValidationResponseModel>();

        public ValidationResponseEnvelope()
        {
            this.Code = HttpStatusCode.BadRequest;
        }
    }
}
