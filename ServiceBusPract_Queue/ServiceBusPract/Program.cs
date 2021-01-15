using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;


namespace ServiceBusPract
{
    class Program
    {
        private static string _bus_connectionstring = Settings._bus_connectionstring;
        private static string _queue_name = Settings._queue_name;
        // For Peer
        //private static IQueueClient _Client = new QueueClient(_bus_connectionstring, _queue_name, ReceiveMode.ReceiveAndDelete);
        // For Recieve and Delete
        private static IQueueClient _Client = new QueueClient(_bus_connectionstring, _queue_name, ReceiveMode.ReceiveAndDelete);

        static async Task Main(string[] args)
        {
            await sendMessage();
            RecieveMessage().GetAwaiter().GetResult();
        }

        static async Task sendMessage()
        {
            Order obj = new Order();
            var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
            _message.TimeToLive = TimeSpan.FromSeconds(30);
            _message.UserProperties.Add("Quantity", obj.quantity);

            Console.WriteLine($"Sending Message: {obj} ");
            await _Client.SendAsync(_message);


        }

        static async Task RecieveMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Receive: ");
            var _options = new MessageHandlerOptions(ExceptionReceived);
            _options.AutoComplete = false;
            _options.MaxConcurrentCalls = 1;
            _options.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);

            _Client.RegisterMessageHandler(Process_Message, _options);
            Console.ReadLine();
        }


        static async Task Process_Message(Message _message, CancellationToken _token)
        {

            Console.WriteLine(Encoding.UTF8.GetString(_message.Body));

            Object qty;
            _message.UserProperties.TryGetValue("Quantity", out qty);
            Console.WriteLine(qty);

           // await _Client.CompleteAsync(_message.SystemProperties.LockToken);

        }

        static Task ExceptionReceived(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

    }
}
