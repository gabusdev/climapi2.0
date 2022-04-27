using System;

namespace Climapi.Core.Entities
{
    public record WeatherForecast
    {
        public string Date { get; init; } = null!;

        public int TempC { get; init; }

        public int TempF { get; init; }

        public long IsDay { get; init; }
        
        public double Lat { get; init; }
        
        public double Lon { get; init; }

        public string Country { get; init; } = null!;

        public string ConditionText { get; init; } = null!;

        public string ConditionIcon { get; init; } = null!;

    }
}
