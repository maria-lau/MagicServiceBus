using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Weather;
using Messages.ServiceBusRequest.Weather.Requests;
using Messages.ServiceBusRequest.Weather.Responses;
using NServiceBus;

namespace AuthenticationService.Communication
{
    partial class ClientConnection
    {
        private ServiceBusResponse WeatherService(WeatherServiceRequest request)
        {
            switch (request.requestType)
            {
                case (WeatherRequest.getWeather):
                    return getWeather((GetWeatherRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request received was:" + request.requestType.ToString());
            }
        }

        private ServiceBusResponse getWeather(GetWeatherRequest request)
        {
            // check that the user is logged in.
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the weather functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Weather");

            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
