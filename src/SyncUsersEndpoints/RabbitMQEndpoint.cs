namespace SyncUsersEndpoints
{
    using System.Threading.Tasks;
    using NServiceBus;

    public class RabbitMqEndpoint
    {
        public static IEndpointInstance Instance { get; private set; }
        public static async Task StartInstance()
        {

            var endpointConfiguration = new EndpointConfiguration("SyncUsers.RabbitMqEndpoint");
            var scanner = endpointConfiguration.AssemblyScanner();
            scanner.ExcludeAssemblies("NServiceBus.Transports.SqlServer");
            scanner.ExcludeTypes(typeof(SqlServerMessageHandler));

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;");
            transport.Transactions(TransportTransactionMode.None);
          
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            Instance = await Endpoint.Start(endpointConfiguration);
            
        }
        
    }
}