using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Messages.Database;
using Messages.DataTypes;
using Messages.NServiceBus.Commands;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Echo.Requests;

using MySql.Data.MySqlClient;

namespace CompanyDirectoryService.Database
{
    //portion of class with methods
    public partial class CompanyDirectoryServiceDatabase : AbstractDatabase
    {
        private CompanyDirectoryServiceDatabase() { }

        public static CompanyDirectoryServiceDatabase getInstance()
        {
            if (instance == null)
            {
                instance = new CompanyDirectoryServiceDatabase();
            }
            return instance;
        }

        public ServiceBusResponse insertNewCompany(CreateAccount info)
        {
            bool result = false;
            string message = "";
            if(openConnection() == true && info.type == AccountType.business)
            {
                string query = @"INSERT INTO company(username, password, address, phonenumber, email, type)" +
                    @"VALUES('" + info.username + @"', '" + info.password +
                    @"', '" + info.address + @"', '" + info.phonenumber + 
                    @"', '" + info.email + @"', '" + info.type.ToString() + @"')";

                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.BeginExecuteNonQuery();
                    result = true; 
                }
                catch(MySqlException e)
                {
                    Messages.Debug.consoleMsg("Unable to complete insert new company into database." +
                        " Error: " + e.Number + e.Message);
                    Messages.Debug.consoleMsg("The query was: " + query);
                    message = e.Message;
                }
                catch(Exception e)
                {
                    Messages.Debug.consoleMsg("Unable to complete insert new comapny into database." +
                        " Error: " + e.Message);
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
    }

    //portion of class with variables
    public  partial class CompanyDirectoryServiceDatabase : AbstractDatabase
    {
        private const String dbname = "companydirectoryservicedb";
        public override string databaseName { get; } = dbname;

        protected static CompanyDirectoryServiceDatabase instance = null;

        //not sure what information needs to be in the table
        protected override Table[] tables { get; } =
        {
            new Table
            (
                dbname,
                "company",
                new Column[]
                {
                    new Column
                    (
                        "username", "VARCHAR(50)",
                        new string[] 
                        {
                            "NOT NULL",
                            "UNIQUE"
                        },true
                    ),
                    new Column
                    (
                        "password", "VARCHAR(50)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "address", "VARCHAR(50)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "phonenumber", "VARCHAR(10)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "email", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE"
                        }, false
                    )
                }
            )
        };
    }
}
