using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ValidationResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_api
{
    public partial class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        //[Required]
        public string Summary { get; set; }

        public string SummaryTwo { get; set; }
    }
}
