using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ServiceBusPract_Topic
{
    class RecieveMessage
    {
        private static string _bus_connectionstring = Settings._bus_connectionstring;
        private static ISubscriptionClient _client = Settings._subscriptionClient;
        private static string _subscription_name = Settings._subscription_name;

        public static async Task recieveMessage()
        {
            ServiceBusConnectionStringBuilder builder = new ServiceBusConnectionStringBuilder(_bus_connectionstring);
            _client = new SubscriptionClient(builder, _subscription_name);

            var _options = new MessageHandlerOptions(ExceptionReceived)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _client.RegisterMessageHandler(Process_Message, _options);
            Console.ReadKey();
        }


        static async Task Process_Message(Message _message, CancellationToken _token)
        {
            Console.WriteLine(Encoding.UTF8.GetString(_message.Body));
            await _client.CompleteAsync(_message.SystemProperties.LockToken);
        }

        static Task ExceptionReceived(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

    }
}
