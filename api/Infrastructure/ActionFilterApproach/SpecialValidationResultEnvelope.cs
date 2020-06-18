using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace api.Infrastructure.ActionFilterApproach
{
    public class SpecialValidationResultEnvelope
    {
        //"type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        //"title": "One or more validation errors occurred.",
        //"status": 400,
        //"traceId": "|41fd7936-40c27cb61ebd8412.",
        public string Type { get; set; }
        public string Title { get; set; }

        public string TraceId { get; set; }

        public HttpStatusCode Status { get; } = HttpStatusCode.BadRequest;

        public IEnumerable<SpecialValidationResult> ErrorsV2 { get; set; }

        public List<SpecialFieldValidationResult> Errors { get; set; } = new List<SpecialFieldValidationResult>();

        public SpecialValidationResultEnvelope(string title, string traceId) {
            this.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            this.Title = title;
            this.TraceId = traceId;
        }
    }
}
