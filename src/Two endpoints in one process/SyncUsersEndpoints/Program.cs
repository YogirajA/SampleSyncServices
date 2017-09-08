namespace SyncUsersEndpoints
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main()
        {
            await RabbitMqEndpoint.StartInstance().ConfigureAwait(false);
            await SqlServerEndpoint.StartInstance().ConfigureAwait(false);
            Console.ReadKey();
        }
    }
}
