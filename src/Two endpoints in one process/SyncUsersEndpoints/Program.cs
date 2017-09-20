namespace SyncUsersEndpoints
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main()
        {
            await Task.WhenAll(RabbitMqEndpoint.StartInstance(), SqlServerEndpoint.StartInstance()).ConfigureAwait(false);
            Console.ReadKey();
        }
    }
}
