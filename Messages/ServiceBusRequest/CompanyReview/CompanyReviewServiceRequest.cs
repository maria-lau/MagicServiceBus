using System;

namespace Messages.ServiceBusRequest.CompanyReview
{
    [Serializable]
    public class CompanyReviewServiceRequest : ServiceBusRequest
    {
        public CompanyReviewServiceRequest(CompanyReviewRequest requestType)
           : base(Service.CompanyReview)
        {
            this.requestType = requestType;
        }

        public CompanyReviewRequest requestType;
    }

    public enum CompanyReviewRequest { GetReview, SaveReview };
}
