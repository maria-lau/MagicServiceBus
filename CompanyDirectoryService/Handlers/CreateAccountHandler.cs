using Messages.NServiceBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using CompanyDirectoryService.Database;
using Messages.ServiceBusRequest.Authentication.Requests;
using Messages.DataTypes.Database.CompanyDirectory;

namespace CompanyDirectoryService.Handlers
{
    class CreateAccountHandler : IHandleMessages<AccountCreated>
    {
        public Task Handle(AccountCreated message, IMessageHandlerContext context)
        {
            //will need to change this
            String[] arr = new String[1];
            arr[0] = message.address;
            if (message.type == Messages.DataTypes.AccountType.business)
            {
                CompanyInstance company = new CompanyInstance(message.username, message.phonenumber, message.email, arr, message.city, message.province);
                CompanyDirectoryServiceDatabase.getInstance().insertNewCompany(company);
            }
            return Task.CompletedTask;
        }
    }
}
