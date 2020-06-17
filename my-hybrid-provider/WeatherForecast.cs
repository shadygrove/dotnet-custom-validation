using my_hybrid_provider.Validation.Annotations;
using System;

namespace my_hybrid_provider
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        //[MinLength(1)]
        public string Summary { get; set; }
    }
}
