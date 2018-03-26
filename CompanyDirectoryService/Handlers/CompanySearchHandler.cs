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
    class CompanySearchHandler : IHandleMessages<CompanySearchRequest>
    {
        public Task Handle(CompanySearchRequest message, IMessageHandlerContext context)
        {
            CompanyList value = CompanyDirectoryServiceDatabase.getInstance().GetCompanyList(message.searchDeliminator);
            String response = "";
            foreach(String s in value.companyNames)
            {
                response += s + ";";
            }
            Messages.Debug.consoleMsg(response);
            return context.Reply(new CompanySearchResponse(true, response, value));
        }
    }
}
