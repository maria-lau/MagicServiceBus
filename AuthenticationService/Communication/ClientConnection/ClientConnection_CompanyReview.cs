using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyReview;
using Messages.ServiceBusRequest.CompanyReview.Requests;
using NServiceBus;

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
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the company reviews functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("CompanyReview");

            // The Request<> funtion itself is an asynchronous operation. However, since we do not want to continue execution until the Request
            // function runs to completion, we call the ConfigureAwait, GetAwaiter, and GetResult functions to ensure that this thread
            // will wait for the completion of Request before continueing. 
            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse saveReview(SaveReviewRequest request)
        {
            // check that the user is logged in.
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the search companies functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("CompanyReview");

            // The Request<> funtion itself is an asynchronous operation. However, since we do not want to continue execution until the Request
            // function runs to completion, we call the ConfigureAwait, GetAwaiter, and GetResult functions to ensure that this thread
            // will wait for the completion of Request before continueing. 
            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
