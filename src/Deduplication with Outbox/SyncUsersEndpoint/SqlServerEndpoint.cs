namespace SyncUsersEndpoints
{
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Persistence.Sql;
    using RabbitMQ.Client;

    public class SqlServerEndpoint
    {
        public static IEndpointInstance Instance { get; private set; }
        public  static async Task StartInstance()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UsersAppDatabase"].ConnectionString;

            var endpointConfiguration = new EndpointConfiguration("SyncUsers.SqlServerEndpoint");
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                .ConnectionString(connectionString);
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

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlVariant(SqlVariant.MsSqlServer);
            persistence.ConnectionBuilder(
                connectionBuilder: () => new SqlConnection(connectionString));
            persistence.SubscriptionSettings().DisableCache();
            endpointConfiguration.DisableFeature<TimeoutManager>();
            endpointConfiguration.EnableOutbox();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.UseSerialization<JsonSerializer>(); 
          //  endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            Instance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false); 
        }
    }
}
