using NServiceBus;

using System;

namespace Messages.ServiceBusRequest.Weather
{
    [Serializable]
    public class WeatherServiceRequest : ServiceBusRequest, IMessage
    {
        public WeatherServiceRequest(WeatherRequest requestType)
            : base(Service.Weather)
        {
            this.requestType = requestType;
        }

        /// <summary>
        /// Indicates the type of request the client is seeking from the Chat Service
        /// </summary>
        public WeatherRequest requestType;
    }

    public enum WeatherRequest { getWeather };
}
