using System;

namespace Messages.ServiceBusRequest.CompanyReview.Responses
{
    [Serializable]
    public class GetReviewResponse : ServiceBusResponse
    {
        public GetReviewResponse(bool result, string response)
            : base(result, response)
        {
            this.reviews = response;
        }

        /// <summary>
        /// A list of companies matching the search criteria given by the client
        /// </summary>
        public string reviews;
    }
}
