using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace CosmosDB_Example
{
    class Program
    {

        private static string database = "appdb";
        private static string containername = "customer";
        private static string endpoint = Settings.endpoint;
        private static string accountkeys = Settings.accountkeys;

        static async Task Main(string[] args)
        {
            //await ReadItem();
            //await ReplaceItem();
            //await CallSP();
            await InsertSP();
            await ReadItem();
            Console.ReadLine();
        }

        private static async Task CreateItem(string name, string city)
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                Database db_conn = cosmos_client.GetDatabase(database);
                Container container_conn = db_conn.GetContainer(containername);
                Random rnd = new Random();

                customer obj = new customer(rnd.Next(1, 9999), name, city);
                obj.id = Guid.NewGuid().ToString();

                ItemResponse<customer> response = await container_conn.CreateItemAsync(obj);

                Console.WriteLine($"Reponse {response.RequestCharge}");

            }
        }

        private static async Task ReadItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                Database db_conn = cosmos_client.GetDatabase(database);
                Container container_conn = db_conn.GetContainer(containername);

                string cosmos_sql = "SELECT * FROM c";

                QueryDefinition query = new QueryDefinition(cosmos_sql);

                FeedIterator<customer> iterator_obj = container_conn.GetItemQueryIterator<customer>(cosmos_sql);

                FeedResponse<customer> customer_obj = await iterator_obj.ReadNextAsync();
                foreach (customer obj in customer_obj)
                {
                    Console.WriteLine($"Customer name is {obj.customername}");
                }

            }
        }

        private static async Task ReplaceItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                Database db_conn = cosmos_client.GetDatabase(database);
                Container container_conn = db_conn.GetContainer(containername);

                PartitionKey pk = new PartitionKey("Costa Mesa");
                string id = "566f0f9b-36cc-447f-aa01-88f0f8006bb8";

                ItemResponse<customer> response = await container_conn.ReadItemAsync<customer>(id, pk);
                customer customer_obj = response.Resource;
                string oldName = customer_obj.customername;

                customer_obj.customername = "Blake";

                response = await container_conn.ReplaceItemAsync<customer>(customer_obj, id, pk);
                Console.WriteLine($"{oldName} changed to {customer_obj.customername}");
            }
        }

        private static async Task CallSP()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                var stored_procedures = container_conn.Scripts;

                PartitionKey key = new PartitionKey(string.Empty);

                var response = await stored_procedures.ExecuteStoredProcedureAsync<string>("demo", key, null);

                Console.WriteLine(response);

            }
        }

        private static async Task InsertSP()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                var stored_procedures = container_conn.Scripts;
                Random rnd = new Random();
                customer customer_obj = new customer(rnd.Next(1, 9999), "Amanda", "Anaheim");

                dynamic[] obj = new dynamic[]
                {
                    new
                    {
                        id = Guid.NewGuid().ToString(),
                        customerid = customer_obj.customerid,
                        customername = customer_obj.customername,
                        city = customer_obj.city
                    }
                };

                PartitionKey key = new PartitionKey(customer_obj.city);
                var response = await stored_procedures.ExecuteStoredProcedureAsync<string>("Insert", key, obj);
            }

        }
    }
}
