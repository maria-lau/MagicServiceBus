using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyReview;
using Messages.ServiceBusRequest.CompanyReview.Requests;


namespace AuthenticationService.Communication
{
    partial class ClientConnection
    {
        private ServiceBusResponse ReviewRequest(CompanyReviewServiceRequest request)
        {
            switch (request.requestType)
            {
                case (CompanyReviewRequest.GetReview):
                    return getReview((GetReviewRequest)request);
                case (CompanyReviewRequest.SaveReview):
                    return saveReview((SaveReviewRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request received was:" + request.requestType.ToString());
            }
        }

        private ServiceBusResponse getReview(GetReviewRequest request)
        {
            // check that the user is logged in.
        }

        private ServiceBusResponse saveReview(SaveReviewRequest request)
        {

        }
    }
}
