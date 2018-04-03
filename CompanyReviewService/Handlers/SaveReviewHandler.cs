using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyReview.Requests;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyReviewService.Handlers
{
    //  class GetReviewHandler : NServiceBus.IHandleMessages<GetReviewRequest>
    public class SaveReviewHandler : NServiceBus.IHandleMessages<SaveReviewRequest>
    {
        HttpClient httpPostRequest = new HttpClient();
        static ILog log = LogManager.GetLogger<SaveReviewRequest>();

        public async Task Handle(SaveReviewRequest message, IMessageHandlerContext context)
        {
            await HandleAsync(message, context);
        }

        public async Task HandleAsync(SaveReviewRequest message, IMessageHandlerContext context)
        {
            ServiceBusResponse returnedMessage;

            try
            {
                string uri = "http://35.188.33.235/api/Review/PostReview";
                var stringContent = new StringContent(message.jsonreview, Encoding.UTF8, "application/json");
                var response = await httpPostRequest.PostAsync(uri, stringContent);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    returnedMessage = new ServiceBusResponse(false, "Successfully saved review for company: " + message.companyName);
                }
                else
                {
                    returnedMessage = new ServiceBusResponse(false, "Failed to save review for company: " + message.companyName);
                }
            }
            catch(Exception e)
            {
                returnedMessage = new ServiceBusResponse(false, e.Message);
            }
            await context.Reply(returnedMessage);
        }
    }
}
