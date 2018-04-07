using Messages.DataTypes.Database.Weather;
using System;


namespace Messages.ServiceBusRequest.Weather.Responses
{
    [Serializable]
    public class GetWeatherResponse : ServiceBusResponse
    {
        public GetWeatherResponse(bool result, string response, WeatherInfo weather)
            :base(result, response)
        {
            this.weather = weather;
        }
        public WeatherInfo weather;
    }
}