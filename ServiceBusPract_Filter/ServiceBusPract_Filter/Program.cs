using System;
using System.Threading.Tasks;

namespace ServiceBusPract_Filter
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
