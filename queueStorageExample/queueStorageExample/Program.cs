using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            await sendMessage();

            await recieveMessage();
            Console.ReadLine();
        }

        static async Task createQueue()
        {
            await queue.CreateIfNotExistsAsync();
        }

        static async Task sendMessage()
        {
            var message = new CloudQueueMessage(createMessage());

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

        static string createMessage()
        {
            WeatherForcast weatherFocast = new WeatherForcast();
            weatherFocast.Date = DateTime.Now;
            weatherFocast.TemperatureCelsius = 23;
            weatherFocast.Summary = "Sunny";

            return JsonSerializer.Serialize(weatherFocast);
        }
    }
}
