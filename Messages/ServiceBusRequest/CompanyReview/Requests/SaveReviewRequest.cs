﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.ServiceBusRequest.CompanyReview
{
    [Serializable]
    public class SaveReviewRequest : CompanyReviewServiceRequest
    {
        public SaveReviewRequest()
             : base(CompanyReviewRequest.SaveReview)
        {

        }
    }
}