using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidator()
        {
            RuleFor(expression: x => x.Summary)
                    //.NotEmpty()
                    .Equal("Butter");
        }
    }
}
