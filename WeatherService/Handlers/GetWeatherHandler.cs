using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.Weather.Requests;
using Messages.ServiceBusRequest.Weather.Responses;
using Messages.DataTypes.Database.Weather;
using System.Net.Http;
using NServiceBus;
using NServiceBus.Logging;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WeatherService.Handlers
{
    class GetWeatherHandler : NServiceBus.IHandleMessages<GetWeatherRequest>
    {
        private static HttpClient client = new HttpClient();

        static ILog log = LogManager.GetLogger<GetWeatherRequest>();

        public  Task Handle(GetWeatherRequest message, IMessageHandlerContext context)
        {
            // get location key
            string apikey = "LcQxhKGWz5UNzaqEUuASyKJk0HHLxfYv";
            string apikey2 = "oLbX3UBxvo3G7zdA4q8AjtKr4RMG3MIV";
            string apikey3 = "5GkBfWDUle2KBTxdCHnP3AKVhmt9nUEH";
            string apikey4 = "ClB7bdIhjEr2kAAA928rGbRQfKCbHdOS";
            string city = message.city;
            string province = message.province;
            string apiurl = "http://dataservice.accuweather.com/locations/v1/cities/search?apikey=" + apikey2 + "&q=" + city + "%2C" +
                                province + "%2CCanada&language=en-ca&details=false&offset=1 HTTP/1.1";

            HttpResponseMessage httpresponse = client.GetAsync(apiurl).Result;
            HttpContent content = httpresponse.Content;
            string citysearchresponse = content.ReadAsStringAsync().Result;
            GetWeatherResponse response;
            // no city found
            if (citysearchresponse.Equals("[]"))
            {
                response = new GetWeatherResponse(false, "", new WeatherInfo());
            }
            else
            {
                string[] tokens = citysearchresponse.Split('"');
                string locationkey = tokens[5];
                // get weather info with location key
                apiurl = "http://dataservice.accuweather.com/currentconditions/v1/" + locationkey + "?apikey=" + apikey2 + "&language=en-ca&details=true";
                HttpResponseMessage httpresponse2 = client.GetAsync(apiurl).Result;
                HttpContent content2 = httpresponse2.Content;
                JavaScriptSerializer js = new JavaScriptSerializer();
                WeatherInfo[] weather = js.Deserialize<WeatherInfo[]>(content2.ReadAsStringAsync().Result);
                response = new GetWeatherResponse(true, "", weather[0]);
            }
            return context.Reply(response);

            // return context.Reply(new GetWeatherResponse(false, "", new WeatherInfo()));
        }
    }
}
