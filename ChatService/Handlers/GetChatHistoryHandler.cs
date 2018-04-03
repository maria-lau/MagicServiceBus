using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using NServiceBus;
using NServiceBus.Logging;
using ChatService.Database;

namespace ChatService.Handlers
{
    class GetChatHistoryHandler : NServiceBus.IHandleMessages<GetChatHistoryRequest>
    {
        static ILog log = LogManager.GetLogger<GetChatHistoryRequest>();

        public Task Handle(GetChatHistoryRequest message, IMessageHandlerContext context)
        {
            ChatDatabase db = ChatDatabase.getInstance();
            GetChatHistoryResponse response = db.retrieveChatHistory(message.getCommand);
            return context.Reply(response);
        }
    }
}
