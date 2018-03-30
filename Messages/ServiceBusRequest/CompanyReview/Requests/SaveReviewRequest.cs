using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.ServiceBusRequest.CompanyReview.Requests
{
    [Serializable]
    public class SaveReviewRequest : CompanyReviewServiceRequest
    {
        public SaveReviewRequest(string companyName, string jsonreview)
             : base(CompanyReviewRequest.SaveReview)
        {
            this.jsonreview = jsonreview;
            this.companyName = companyName;
        }

        public string jsonreview;
        public string companyName;
    }
}