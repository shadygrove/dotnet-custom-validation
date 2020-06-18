using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ValidationResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_api
{
    public partial class WeatherForecast : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (TemperatureC < -20 || TemperatureC > 100)
            {
                yield return MyValidationResultFactory.Create(MyValidatorType.OUT_OF_RANGE, "The TemperatureC was out of range", nameof(TemperatureC));
                yield return MyValidationResultFactory.Create(MyValidatorType.OTHER, "The TemperatureC was out of range", nameof(TemperatureC));
            }

            if (String.IsNullOrEmpty(Summary))
            {
                yield return MyValidationResultFactory.Create(MyValidatorType.REQUIRED, "Summary is required", nameof(Summary));
            }

            // NOTE: This only seems to getMyValidationTypes called if the property validation attributes are all valid
            if (SummaryTwo != Summary)
            {
                var fields = new string[] { nameof(Summary), nameof(SummaryTwo) };

                yield return MyValidationResultFactory.Create(MyValidatorType.MUST_MATCH, "Summary must match SummaryTwo", fields);
            }
        }
    }
}
