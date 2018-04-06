using System;

namespace Messages.DataTypes.Database.Weather
{
    [Serializable]
    public class WeatherObject
    {
        public WeatherInfo[] weather { get; set; } = null;
    }

}