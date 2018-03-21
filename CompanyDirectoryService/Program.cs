using System;
using System.Threading;
using System.Threading.Tasks;


using NServiceBus;


namespace CompanyDirectoryService
{
    class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }
    }
}
