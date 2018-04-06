using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.Weather.Requests;
using Messages.ServiceBusRequest.Weather.Responses;
using Messages.DataTypes.Database.Weather;
using System.Net.Http;
using NServiceBus;
using NServiceBus.Logging;
using Newtonsoft.Json;

namespace WeatherService.Handlers
{
    class GetWeatherHandler : NServiceBus.IHandleMessages<GetWeatherRequest>
    {
        private static HttpClient client = new HttpClient();

        static ILog log = LogManager.GetLogger<GetWeatherRequest>();

        public async Task Handle(GetWeatherRequest message, IMessageHandlerContext context)
        {
            await HandleAsync(message, context);
        }
        public async Task HandleAsync(GetWeatherRequest message, IMessageHandlerContext context)
        {
            // get location key
            string apikey = "LcQxhKGWz5UNzaqEUuASyKJk0HHLxfYv";
            string apikey2 = "oLbX3UBxvo3G7zdA4q8AjtKr4RMG3MIV";
            string apikey3 = "5GkBfWDUle2KBTxdCHnP3AKVhmt9nUEH";
            string city = message.city;
            string province = message.province;
            string apiurl = "http://dataservice.accuweather.com/locations/v1/cities/search?apikey=" + apikey3 + "&q=" + city + "%2C" +
                                province + "%2CCanada&language=en-ca&details=false&offset=1 HTTP/1.1";

            HttpResponseMessage response = client.GetAsync(apiurl).Result;
            HttpContent content = response.Content;
            string citysearchresponse = content.ReadAsStringAsync().Result;
            WeatherObject weather = new WeatherObject();
            GetWeatherResponse response3;
            // no city found
            if (citysearchresponse.Equals("[]"))
            {
                response3 = new GetWeatherResponse(false, "", weather);
            }
            else
            {
                string[] tokens = citysearchresponse.Split('"');
                string locationkey = tokens[5];
                // get weather info with location key
                apiurl = "http://dataservice.accuweather.com/currentconditions/v1/" + locationkey + "?apikey=" + apikey3 + "&language=en-ca&details=true%20HTTP/1.1";
                HttpResponseMessage response2 = client.GetAsync(apiurl).Result;
                HttpContent content2 = response2.Content;
                string jsonString = content2.ReadAsStringAsync().Result;
                weather = JsonConvert.DeserializeObject<WeatherObject>(jsonString);
                response3 = new GetWeatherResponse(true, "", weather);
            }
            await context.Reply(response3);
        }
    }
}
