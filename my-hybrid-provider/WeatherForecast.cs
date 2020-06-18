using my_hybrid_provider.Validation;
using my_hybrid_provider.Validation.Annotations;
using MyValidation.Core.V1.ResponseModels;
using MyValidation.Core.V1.ValidationResults;
using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ValidationResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_hybrid_provider
{
    public class WeatherForecast : IValidatableObject
    {
        public DateTime Date { get; set; }

        [MyRange(-20, 120)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [MyMinLength(2)]
        [Required(ErrorMessage = "DotNetCore: Summary is required")]           // Built-in AspNetCore Required
        [MaxLength(8, ErrorMessage = "DotNetCore: Summary max length exceeded")]       // Built-in AspNetCore MaxLength
        public string Summary { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // NOTE: All annotation validations MUST PASS
            // before this method will get called!!!
            if (TemperatureC > 100)
            {
                //var result = new MyValidationResult("IValidatableObject: value should be greater than 100", new string[] { nameof(TemperatureC) });
                //result.ErrorCode = ErrorCode.RangeError;

                var result = MyValidationResultFactory.Create(MyValidationTypes.OUT_OF_RANGE, nameof(TemperatureC));

                yield return result;
            }
        }
    }
}
