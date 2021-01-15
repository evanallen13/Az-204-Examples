using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusPract_Filter
{
    class SendMessage
    {
        private static string _bus_connectionstring = Settings._bus_connectionstring;
        private static string _topic_name = Settings._topic_name;
        private static ITopicClient _client;
        private static string[] Exams = new string[] { "AZ-900", "AZ-300", "AZ-400", "AZ-500", "AZ-204" };

        public static async Task sendMessage()
        {
            _client = new TopicClient(_bus_connectionstring, _topic_name);

            for (int i = 0; i < 5; i++)
            {
                Order obj = new Order();
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                _message.MessageId = $"{i}";
                _message.UserProperties.Add("Category", Exams[i]);
                await _client.SendAsync(_message);
                Console.WriteLine($"Sending Message : {obj.Id} ");


            }
            Console.ReadKey();
            await _client.CloseAsync();

        }
    }
}
