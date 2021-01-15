using System;
using System.Threading;
using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusPract_Topic
{
    class Program
    {
        static async Task Main(string[] args)
        {

           //await SendMessage.sendMessage();

            RecieveMessage.recieveMessage().GetAwaiter().GetResult();

        }
    
        

    }
}
