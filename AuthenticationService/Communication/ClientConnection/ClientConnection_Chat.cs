using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using NServiceBus;

namespace AuthenticationService.Communication
{
    partial class ClientConnection
    {
        private ServiceBusResponse ChatContactsRequest(ChatServiceRequest request)
        {
            switch (request.requestType)
            {
                case (ChatRequest.getChatHistory):
                    return getChatHistory((GetChatHistoryRequest)request);
                case (ChatRequest.getChatContacts):
                    return getChatContacts((GetChatContactsRequest)request);
                case (ChatRequest.sendMessage):
                    return sendMessage((SendMessageRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request received was:" + request.requestType.ToString());
            }
        }

        private ServiceBusResponse getChatHistory(GetChatHistoryRequest request)
        {
            // check that the user is logged in.
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the chat functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Chat");

            return requestingEndpoint.Request<GetChatHistoryResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse getChatContacts(GetChatContactsRequest request)
        {
            // check that the user is logged in.
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the chat functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Chat");

            return requestingEndpoint.Request<GetChatContactsResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse sendMessage(SendMessageRequest request)
        {
            // check that the user is logged in.
            if (authenticated == false)
            {
                return new ServiceBusResponse(false, "Error: You must be logged in to use the chat functionality.");
            }

            // This class indicates to the request function where 
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Chat");

            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
