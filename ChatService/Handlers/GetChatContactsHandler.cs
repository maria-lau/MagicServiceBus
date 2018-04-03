using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using System.Net.Http;
using System.Web.Script.Serialization;
using NServiceBus;
using NServiceBus.Logging;
using ChatService.Database;

namespace ChatService.Handlers
{
    class GetChatContactsHandler : IHandleMessages<GetChatContactsRequest>
    {

        static ILog log = LogManager.GetLogger<GetChatContactsRequest>();

        public Task Handle(GetChatContactsRequest message, IMessageHandlerContext context)
        {
            ChatDatabase db = ChatDatabase.getInstance();
            GetChatContactsResponse response = db.retrieveChatContacts(message.getCommand.usersname);
            return context.Reply(response);
        }
    }
}