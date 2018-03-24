using Messages.NServiceBus.Commands;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using Messages.DataTypes.Database.CompanyDirectory;

using NServiceBus;

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
                    return getCompanyInfo((GetCompanyInfoRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request received was:" + request.requestType.ToString());
            }
        }

        private ServiceBusResponse companySearch(CompanySearchRequest request)
        {
            //Check that user is logged in
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the search companies functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("CompanyDirectory");

            // The Request<> funtion itself is an asynchronous operation. However, since we do not want to continue execution until the Request
            // function runs to completion, we call the ConfigureAwait, GetAwaiter, and GetResult functions to ensure that this thread
            // will wait for the completion of Request before continueing. 
            return requestingEndpoint.Request<CompanySearchResponse>(request, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse getCompanyInfo(GetCompanyInfoRequest request)
        {
            return new ServiceBusResponse(true, "Getting Company Info");
        }
    }
}
