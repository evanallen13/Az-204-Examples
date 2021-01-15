using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusPract_Topic
{
    class SendMessage
    {
        private static string _bus_connectionstring = Settings._bus_connectionstring;
        private static string _topic_name = Settings._topic_name;
        private static ITopicClient _client = Settings._queueClient;
        public static async Task sendMessage()
        {
            for (int i = 0; i < 10; i++)
            {
                Order obj = new Order();
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                Console.WriteLine($"Sending Message : {obj.Id} ");
                await _client.SendAsync(_message);


            }
            Console.ReadKey();
            await _client.CloseAsync();
        }

        static void sayHello()
        {
            Console.WriteLine("HELLO WORLD");
        }

    }
}
