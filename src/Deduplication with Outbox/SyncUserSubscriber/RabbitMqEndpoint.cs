namespace SyncUserSubscriber
{
    using System.Threading.Tasks;
    using Messages.V1;
    using NServiceBus;

    public class RabbitMqEndpoint
    {
        public static IEndpointInstance Instance { get; private set; }
        public static async Task StartInstance()
        {
            var endpointConfiguration = new EndpointConfiguration("SyncUsers.RabbitMqEndpoint");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
          	transport.Transactions(TransportTransactionMode.None);
            //transport.DelayedDelivery().DisableTimeoutManager();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableOutbox();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.RegisterComponents(configure =>
            {
                configure.ConfigureComponent<AccountsContext>(DependencyLifecycle.InstancePerUnitOfWork);
            });
            Instance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        }

    }
}