using CompanyDirectoryService.Database;
using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDirectoryService.Handlers
{
    class GetCompanyInfoHandler : IHandleMessages<GetCompanyInfoRequest>
    {
        public Task Handle(GetCompanyInfoRequest message, IMessageHandlerContext context)
        {
            CompanyInstance instance = CompanyDirectoryServiceDatabase.getInstance().GetCompanyInfo(message);
            String response = "";

            return context.Reply(new GetCompanyInfoResponse(true, response, instance));
        }
    }
}
