namespace SyncUsersEndpoints
{
    using System.Threading.Tasks;
    using NServiceBus;
    using RabbitMQ.Client;

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
          	transport.Transactions(TransportTransactionMode.None);
            //transport.DelayedDelivery().DisableTimeoutManager();
         
          
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.RegisterComponents(configure =>
            {
                configure.ConfigureComponent<AccountsContext>(DependencyLifecycle.InstancePerUnitOfWork);
            });
            Instance = await Endpoint.Start(endpointConfiguration);
            
        }
        
    }
}