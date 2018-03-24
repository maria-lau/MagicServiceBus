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

            CompanySearchEvent searchEvent= new CompanySearchEvent
            {
                searchDelimiter = request.searchDeliminator,
                username = username
            };

            //This function publishes the EchoEvent class. All endpoint instances that subscribed to these events prior
            //to the event being published will have their respictive handler functions called with the EchoEvent object
            //as one of the parameters
            eventPublishingEndpoint.Publish(searchEvent);
            return new CompanySearchResponse(true, request.searchDeliminator, new CompanyList());
        }

        private ServiceBusResponse getCompanyInfo(GetCompanyInfoRequest request)
        {
            return new ServiceBusResponse(true, "Getting Company Info");
        }
    }
}
