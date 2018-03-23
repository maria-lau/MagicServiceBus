using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Communication
{
    partial class ClientConnection
    {
        private ServiceBusResponse companyRequest(CompanyDirectoryServiceRequest request)
        {
            switch (request.requestType)
            {
                case (CompanyDirectoryRequest.CompanySearch):
                    return companySearch((CompanySearchRequest)request);
                case (CompanyDirectoryRequest.GetCompanyInfo):
                    return getCompany((GetCompanyInfoRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request reveived was:" + request.requestType.ToString());

            }
        }

        private ServiceBusResponse companySearch(CompanySearchRequest request)
        {
            return null;
        }

        private ServiceBusResponse getCompany(GetCompanyInfoRequest request)
        {
            return null;
        }
    }

    partial class ClientConnection
    {

    }
}
