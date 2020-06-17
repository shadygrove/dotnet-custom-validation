using my_hybrid_provider.Validation.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace my_hybrid_provider
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        [MyRange(-20, 120)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [MyMinLength(2)]
        [Required]           // Built-in AspNetCore Required
        [MaxLength(8)]       // Built-in AspNetCore MaxLength
        public string Summary { get; set; }
    }
}
