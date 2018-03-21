using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Messages.Database;
using Messages.DataTypes;
using Messages.NServiceBus.Events;
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

        //will need to implement functions for interacting with DB
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

                }
            )
        };
    }
}
