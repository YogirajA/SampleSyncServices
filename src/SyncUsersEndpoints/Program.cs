﻿namespace SyncUsersEndpoints
{
    using System;
    using System.Threading.Tasks;


    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task AsyncMain()
        {
            await SqlServerEndpoint.StartInstance().ConfigureAwait(false);
            await RabbitMqEndpoint.StartInstance().ConfigureAwait(false);
       
        }
    
    }
}
