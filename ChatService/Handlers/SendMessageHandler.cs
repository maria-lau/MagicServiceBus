using System;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using System.Net.Http;
using System.Web.Script.Serialization;
using NServiceBus;
using NServiceBus.Logging;
using ChatService.Database;
using Messages.ServiceBusRequest;

namespace ChatService.Handlers
{
    class SendMessageHandler : NServiceBus.IHandleMessages<SendMessageRequest>
    {
        private static HttpClient client = new HttpClient();
        static ILog log = LogManager.GetLogger<SendMessageRequest>();

        public async Task Handle(SendMessageRequest message, IMessageHandlerContext context)
        {
            await HandleAsync(message, context);
        }

        public async Task HandleAsync(SendMessageRequest message, IMessageHandlerContext context)
        {
            ChatDatabase db = ChatDatabase.getInstance();
            ServiceBusResponse response = db.insertNewChatMessage(message.message);
            await context.Reply(response);
        }
    }
}
