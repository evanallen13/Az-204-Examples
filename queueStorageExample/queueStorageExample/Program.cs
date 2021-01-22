using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;

namespace queueStorageExample
{
    class Program
    {

        private static string ConnectionString = Settings.ConnectionString;
        private static CloudStorageAccount account = CloudStorageAccount.Parse(ConnectionString);
        private static CloudQueueClient client = account.CreateCloudQueueClient();
        private static CloudQueue queue = client.GetQueueReference("myqueue");

        static async Task Main(string[] args)
        {
         
            for(int i = 0; i <= 10; i++)
            {
                await sendMessage();
            }
            await recieveMessage();
            Console.ReadLine();
        }

        static async Task createQueue()
        {
            await queue.CreateIfNotExistsAsync();
        }

        static async Task sendMessage()
        {
            var message = new CloudQueueMessage($"your message here {Guid.NewGuid()}");

            await queue.AddMessageAsync(message);
        }

        static async Task recieveMessage()
        {

            while (true)
            {
                CloudQueueMessage message = await queue.GetMessageAsync();

                if (message != null)
                {
                    Console.WriteLine(message.AsString);

                    await queue.DeleteMessageAsync(message);
                }
                else
                {
                    break;
                }
            }


        }
    }
}
