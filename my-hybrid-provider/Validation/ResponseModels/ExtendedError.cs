using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation.ResponseModels
{
    public class ExtendedError
    {
        public string ErrorMessage { get; set; }

        public ErrorCode ErrorCode { get; set; }
    };
}
