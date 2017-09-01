namespace SyncUsersEndpoints
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;

    public class SqlServerMessageHandler : IHandleMessages<NewUser>
    {
        public async Task Handle(NewUser message, IMessageHandlerContext context)
        {
            await RabbitMqEndpoint.Instance.Send("SyncUsers.RabbitMqEndpoint",message);
       
        }
    }
}