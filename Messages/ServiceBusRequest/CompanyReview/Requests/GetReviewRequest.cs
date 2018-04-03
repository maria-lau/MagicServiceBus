using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.ServiceBusRequest.CompanyReview.Requests
{
    [Serializable]
    public class GetReviewRequest : CompanyReviewServiceRequest
    {
        public GetReviewRequest(string companyName)
             : base(CompanyReviewRequest.GetReview)
        {
            this.companyName = companyName;
        }

        public string companyName;
    }
}
