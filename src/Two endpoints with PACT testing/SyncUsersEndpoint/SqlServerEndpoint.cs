namespace SyncUsersEndpoints
{
    using System.Threading.Tasks;
    using NServiceBus;
    public class SqlServerEndpoint
    {
        public static IEndpointInstance Instance { get; private set; }
        public  static async Task StartInstance()
        {
            var endpointConfiguration = new EndpointConfiguration("SyncUsers.SqlServerEndpoint");
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                .ConnectionStringName("UsersAppDatabase");
            transport.Transactions(TransportTransactionMode.None);

            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            Instance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false); ;
        }
    }
}
