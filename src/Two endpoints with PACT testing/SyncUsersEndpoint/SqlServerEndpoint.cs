namespace SyncUsersEndpoints
{
    using System.Threading.Tasks;
    using NServiceBus;
    using RabbitMQ.Client;

    public class SqlServerEndpoint
    {
        public static IEndpointInstance Instance { get; private set; }
        public  static async Task StartInstance()
        {
            var endpointConfiguration = new EndpointConfiguration("SyncUsers.SqlServerEndpoint");
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                .ConnectionStringName("UsersAppDatabase");
            transport.Transactions(TransportTransactionMode.None);
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            endpointConfiguration.RegisterComponents(
                registration: configureComponents =>
                {

                    configureComponents.ConfigureComponent(
                        componentFactory: () => factory.CreateConnection().CreateModel(),
                        dependencyLifecycle: DependencyLifecycle.InstancePerUnitOfWork);

                });
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            Instance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false); ;
        }
    }
}
