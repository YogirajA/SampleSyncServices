namespace SyncUsersEndpoints
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;

    public class SqlServerMessageHandler : IHandleMessages<NewUser>
    {
        public Task Handle(NewUser message, IMessageHandlerContext context)
        {
            RabbitMqEndpoint.Instance.Send("SyncUsers.RabbitMqEndpoint",message);
            return Task.CompletedTask;
        }
    }
}