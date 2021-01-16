using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;

namespace ServiceBusPract_CreateQueue
{
    class Program
    {
        private static string _bus_connectionstring = Settings._bus_connectionstring;
        private static string _queue_name = $"{Settings._queue_name}_Three";
        static async Task Main(string[] args)
        {
            CreateQueue(_queue_name).Wait();
            Console.WriteLine("Queue created");
        }

        static async Task CreateQueue(string p_queuename)
        {
            ManagementClient _client = new ManagementClient(_bus_connectionstring);

            var _description = new QueueDescription(p_queuename)
            {
                EnableBatchedOperations = false,
                LockDuration = TimeSpan.FromMinutes(2),
                MaxDeliveryCount = 2,
                DefaultMessageTimeToLive = TimeSpan.FromMinutes(2)
            };
            var queue = await _client.CreateQueueAsync(_description);
        }
    }
}
