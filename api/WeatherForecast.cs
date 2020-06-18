using api.Infrastructure;
using api.Infrastructure.ActionFilterApproach;
using api.Infrastructure.DataAnnotationsModelValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api
{
    public class WeatherForecast : IValidatableObject
    {
        public DateTime Date { get; set; }

        [Range(-20, 100)]
        [MyCustomValidationAttribute]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [MinLength(1)]
        [MyCustomValidationAttribute]
        public string Summary { get; set; }

        public string SummaryTwo { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    // NOTE: This only seems to get called if the property validation attributes are all valid
        //    if (SummaryTwo != Summary)
        //    {
        //        yield return new CustomValidationResult("SumaryTwo must match Summary",
        //                    new string[] { "SummaryTwo" });
        //    }

        //}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TemperatureC < -20 || TemperatureC > 100)
            {
                yield return new SpecialValidationResult("Out of range", "OUT_OF_RANGE", new string[] { nameof(TemperatureC) });
                yield return new SpecialValidationResult("Other error", "OTHER_ERROR", new string[] { nameof(TemperatureC) });
            }

            if (String.IsNullOrEmpty(Summary))
            {
                yield return new SpecialValidationResult("Summary is required", "REQUIRED", new string[] { nameof(Summary) });
            }

            // NOTE: This only seems to get called if the property validation attributes are all valid
            if (SummaryTwo != Summary)
            {
                yield return new SpecialValidationResult("SummaryTwo must match Summary", "MUST_MATCH_FIELD", new string[] { nameof(SummaryTwo) });
            }

        }

        //public IEnumerable<SpecialValidationResult> ValidateSpecial()
        //{
        //    if (TemperatureC < -20 || TemperatureC > 100)
        //    {
        //        yield return new SpecialValidationResult("TemperatureC", "OUT_OF_RANGE", "Out of range");
        //    }

        //    if (String.IsNullOrEmpty(Summary))
        //    {
        //        yield return new SpecialValidationResult("Summary", "REQUIRED", "Summary is required");
        //    }
        //}
    }
}
