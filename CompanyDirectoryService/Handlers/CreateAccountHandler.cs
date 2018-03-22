using Messages.NServiceBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using CompanyDirectoryService.Database;

namespace CompanyDirectoryService.Handlers
{
    class CreateAccountHandler : IHandleMessages<AccountCreated>
    {
        public Task Handle(AccountCreated message, IMessageHandlerContext context)
        {
            CompanyDirectoryServiceDatabase.getInstance().
            return Task.CompletedTask;
        }
    }
}
