using System;
using Messages.NServiceBus.Commands;
using NServiceBus;

namespace Messages.ServiceBusRequest.Weather.Requests
{
    [Serializable]
    public class GetWeatherRequest: WeatherServiceRequest
    {
        public GetWeatherRequest(string city, string province)
              : base(WeatherRequest.getWeather)
        {
            this.city = city;
            this.province = province;
        }

        public string city;
        public string province;
    }
}
