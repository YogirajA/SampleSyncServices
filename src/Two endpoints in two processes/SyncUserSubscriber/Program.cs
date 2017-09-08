namespace SyncUserSubscriber
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task AsyncMain()
        {
            await RabbitMqEndpoint.StartInstance().ConfigureAwait(false);
        }
    }
}
