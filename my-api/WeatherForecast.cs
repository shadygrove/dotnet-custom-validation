using my_api.Validation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_api
{
    public class WeatherForecast : IValidatableObject
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public string SummaryTwo { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            MyValidationResult result = null;

            if (TemperatureC < -20 || TemperatureC > 100)
            {
                yield return MyValidationResultFactory.Create(MyValidationTypes.OUT_OF_RANGE, nameof(TemperatureC));
                yield return MyValidationResultFactory.Create(MyValidationTypes.OTHER, nameof(TemperatureC));
            }

            if (String.IsNullOrEmpty(Summary))
            {
                yield return MyValidationResultFactory.Create(MyValidationTypes.REQUIRED, nameof(Summary));
            }

            // NOTE: This only seems to getMyValidationTypes called if the property validation attributes are all valid
            if (SummaryTwo != Summary)
            {
                var fields = new string[] { nameof(Summary), nameof(SummaryTwo) };

                yield return MyValidationResultFactory.Create(MyValidationTypes.MUST_MATCH, fields);
            }
        }
    }
}
