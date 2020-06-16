using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Validation.Models
{
    public enum MyValidationTypes
    {
        REQUIRED,
        MIN,
        MAX,
        MIN_LENGTH,
        MAX_LENGTH,
        OUT_OF_RANGE,
        MUST_MATCH,
        OTHER 
    }
}
