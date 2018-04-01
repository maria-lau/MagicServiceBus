using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.Chat.Requests;
using NServiceBus;
using NServiceBus.Logging;
using ChatService.Database;
using Messages.ServiceBusRequest;

namespace ChatService.Handlers
{
    class SendMessageHandler : IHandleMessages<SendMessageRequest>
    {
        static ILog log = LogManager.GetLogger<SendMessageRequest>();

        public  Task Handle(SendMessageRequest message, IMessageHandlerContext context)
        {
            ChatDatabase db = ChatDatabase.getInstance();
            ServiceBusResponse response = db.insertNewChatMessage(message.message);
            return context.Reply(response);
        }
    }
}
