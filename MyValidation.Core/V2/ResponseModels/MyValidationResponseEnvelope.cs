using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyValidation.Core.V2.ResponseModels
{
    public class MyValidationResponseEnvelope
    {
        public string Message { get; set; }

        public HttpStatusCode Code { get; private set; }

        public List<MyValidationResponseModel> Errors { get; set; } = new List<MyValidationResponseModel>();

        public MyValidationResponseEnvelope()
        {
            this.Code = HttpStatusCode.BadRequest;
        }
    }
}
