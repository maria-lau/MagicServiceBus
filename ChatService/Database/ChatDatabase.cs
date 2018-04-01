using System;
using System.Collections.Generic;
using Messages.Database;
using Messages.DataTypes.Database.Chat;
using Messages.NServiceBus.Commands;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat.Responses;
using MySql.Data.MySqlClient;

namespace ChatService.Database
{
    //portion of class with methods
    public partial class ChatDatabase : AbstractDatabase
    {
        private ChatDatabase() { }

        public static ChatDatabase getInstance()
        {
            if (instance == null)
            {
                instance = new ChatDatabase();
            }
            return instance;
        }

        public ServiceBusResponse insertNewChatContact(string user1, string user2)
        {
            bool result = false;
            string message = "";
            string query = "INSERT INTO " + dbname + ".chatcontacts(usersname, contactname) " + "VALUES ('" + user1 + "','" + user2 + "');";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (MySqlException e)
                {
                    Messages.Debug.consoleMsg("Unable to complete insert new chat contact into database." +
                        " Error :" + e.Number + e.Message);
                    Messages.Debug.consoleMsg("The query was:" + query);
                    message = e.Message;
                }
                catch (Exception e)
                {
                    Messages.Debug.consoleMsg("Unable to complete insert new contact into database." +
                        " Error:" + e.Message);
                    message = e.Message;
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                message = "Unable to connect to database";
            }

            return new ServiceBusResponse(result, message);
        }


        public GetChatContactsResponse retrieveChatContacts(string usersname)
        {
            string query = @"SELECT * FROM " + dbname + ".chatcontacts "
                    + "WHERE usersname='" + usersname + "';";

            bool result = false;
            string message = "";
            GetChatContacts contacts = new GetChatContacts();
            contacts.usersname = usersname;
            contacts.contactNames = new List<string>();

            if (openConnection() == true)
            {
                result = true;

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                // if tuples are returned
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        contacts.contactNames.Add(dataReader.GetString(2));
                    }

                    dataReader.Close();
                    closeConnection();
                    return new GetChatContactsResponse(result, message, contacts);
                }
                // no tuples returned
                else
                {
                    dataReader.Close();
                    closeConnection();
                    return new GetChatContactsResponse(result, message, contacts);
                }
            }
            else
            {
                result = false;
                message = "Could not connect to database.";
            }
            return new GetChatContactsResponse(result, message, contacts);
        }


        public ServiceBusResponse insertNewChatMessage(ChatMessage chatmessage)
        {
            bool result = false;
            string message = "";
            string sender = chatmessage.sender;
            string receiver = chatmessage.receiver;
            int timeStamp = chatmessage.unix_timestamp;
            string messagecontent = chatmessage.messageContents;

            string query = "INSERT INTO " + dbname + ".chatmessages(sender, receiver, timestamp, message) " +
                            "VALUES ('" + sender + "','" + receiver + "','" + timeStamp + "','" + messagecontent + "');";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    result = true;

                    // now check if this is a new chat contact and update chat contacts if needed
                    query = @"SELECT * FROM " + dbname + ".chatcontacts " + "WHERE usersname='" + sender + "' AND contactname = '" + receiver + "';";
                    command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    // if chat contact doesn't exist
                    if (!(dataReader.HasRows))
                    {
                        insertNewChatContact(sender, receiver);
                    }

                    query = @"SELECT * FROM " + dbname + ".chatcontacts " + "WHERE usersname='" + receiver + "' AND contactname = '" + sender + "';";
                    command = new MySqlCommand(query, connection);
                    dataReader = command.ExecuteReader();

                    if (!(dataReader.HasRows))
                    {
                        insertNewChatContact(receiver, sender);
                    }
                    dataReader.Close();
                }
                catch (MySqlException e)
                {
                    Messages.Debug.consoleMsg("Unable to complete insert new chat message into database." +
                        " Error :" + e.Number + e.Message);
                    Messages.Debug.consoleMsg("The query was:" + query);
                    message = e.Message;
                }
                catch (Exception e)
                {
                    Messages.Debug.consoleMsg("Unable to complete insert new chat message into database." +
                        " Error:" + e.Message);
                    message = e.Message;
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                message = "Unable to connect to database";
            }

            return new ServiceBusResponse(result, message);
        }

        public GetChatHistoryResponse retrieveChatHistory(GetChatHistory getRequest)
        {
            bool result = false;
            string message = "";
            string sender = getRequest.history.user1;
            string receiver = getRequest.history.user2;

            string query = @"SELECT * FROM " + dbname + ".chatmessages "
                    + "WHERE (sender='" + sender + "' AND receiver='" + receiver + "') OR (sender='" + receiver + "' AND receiver='" + sender + "') ORDER BY id ASC;";

            GetChatHistory chathistory = new GetChatHistory()
            {
                history = new ChatHistory
                {
                    user1 = sender,
                    user2 = receiver,
                    messages = new List<ChatMessage>()
                }      
            };

            if (openConnection() == true)
            {
                result = true;
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                // if tuples are returned
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        ChatMessage temp = new ChatMessage();
                        temp.sender = dataReader.GetString(1);
                        temp.receiver = dataReader.GetString(2);
                        temp.unix_timestamp = dataReader.GetInt32(3);
                        temp.messageContents = dataReader.GetString(4);
                        chathistory.history.messages.Add(temp);
                    }

                    dataReader.Close();
                    closeConnection();
                    return new GetChatHistoryResponse(result, message, chathistory);
                }
                // no tuples returned
                else
                {
                    dataReader.Close();
                    closeConnection();
                    return new GetChatHistoryResponse(result, message, chathistory);
                }
            }
            else
            {
                result = false;
                message = "Could not connect to database.";
            }
            return new GetChatHistoryResponse(result, message, chathistory);
        }
    }

    //portion of class with variables
    public partial class ChatDatabase : AbstractDatabase
    {
        private const String dbname = "chatdb";
        public override string databaseName { get; } = dbname;

        protected static ChatDatabase instance = null;


        protected override Table[] tables { get; } =
        {
            new Table
            (
                dbname,
                "chatcontacts",
                new Column[]
                {
                    new Column
                    (
                        "id", "INT(64)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE",
                            "AUTO_INCREMENT"
                        }, true
                    ),
                    new Column
                    (
                        "usersname", "VARCHAR(30)",
                        new string[]
                        {
                            "NOT NULL",
                        },true
                    ),
                    new Column
                    (
                        "contactname", "VARCHAR(30)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    )
                }
            ),
            new Table
            (
                dbname,
                "chatmessages",
                new Column[]
                {
                    new Column
                    (
                        "id", "INT(64)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE",
                            "AUTO_INCREMENT"
                        }, true
                    ),
                    new Column
                    (
                        "sender", "VARCHAR(30)",
                        new string[]
                        {
                            "NOT NULL",
                        },true
                    ),
                    new Column
                    (
                        "receiver", "VARCHAR(30)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "timestamp", "INT(64)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "message", "VARCHAR(300)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    )
                }
            )
        };
    }

}





