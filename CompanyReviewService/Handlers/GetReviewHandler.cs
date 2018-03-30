using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.CompanyReview.Requests;
using Messages.ServiceBusRequest.CompanyReview.Responses;
using System.Net.Http;
using System.Web.Script.Serialization;
using NServiceBus;
using NServiceBus.Logging;
using CompanyReviewService.Models;

namespace CompanyReviewService.Handlers
{
    class GetReviewHandler : NServiceBus.IHandleMessages<GetReviewRequest>
    {
        private static HttpClient client = new HttpClient();

        static ILog log = LogManager.GetLogger<GetReviewRequest>();

        public async Task Handle(GetReviewRequest message, IMessageHandlerContext context)
        {
            await HandleAsync(message, context);
        }
        public async Task HandleAsync(GetReviewRequest message, IMessageHandlerContext context)
        {
            string apiurl = "http://35.188.167.20/api/review/getreview/{companyName:\"" + message.companyName + "\"}";

            HttpResponseMessage response = client.GetAsync(apiurl).Result;
            HttpContent content = response.Content;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ReviewInfo[] reviews = js.Deserialize<ReviewInfo[]>(content.ReadAsStringAsync().Result);
            String stringReviews = "";
            for (int i = 0; i < reviews.Length; i++)
            {
                stringReviews = stringReviews + reviews[i].username + "<br />Wrote a review for <a style=color:#1185f9>" + reviews[i].companyName
                                + "</a><br /><a style=color:gold>&#9733</a>Rating: " + reviews[i].stars + "<br />" + reviews[i].review
                                + "<br />Time: " + reviews[i].timestamp + "<br /><br /><br />";
            }

            GetReviewResponse response2 = new GetReviewResponse(false, stringReviews);
            await context.Reply(response2);
        }
    }
}
