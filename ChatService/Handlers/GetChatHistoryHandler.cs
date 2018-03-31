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
    class GetChatHistoryHandler : NServiceBus.IHandleMessages<GetChatHistoryRequest>
    {
        private static HttpClient client = new HttpClient();

        static ILog log = LogManager.GetLogger<GetChatHistoryRequest>();

        public async Task Handle(GetChatHistoryRequest message, IMessageHandlerContext context)
        {
            await HandleAsync(message, context);
        }
        public async Task HandleAsync(GetChatHistoryRequest message, IMessageHandlerContext context)
        {
            ChatDatabase db = ChatDatabase.getInstance();
            GetChatHistoryResponse response = (GetChatHistoryResponse)db.retrieveChatHistory(message.getCommand);
            await context.Reply(response);
        }
    }
}
