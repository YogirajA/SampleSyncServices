namespace SyncUserSubscriber
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;

    public class RabbitMessageHandler : IHandleMessages<NewUser>
    {
        private readonly AccountsContext _context;

        public RabbitMessageHandler(AccountsContext context)
        {
            _context = context;
        }
        public async Task Handle(NewUser message, IMessageHandlerContext context)
        {
            var user = new User
            {
                CreatedOn = message.CreatedOn,
                ModifiedOn = message.ModifiedOn,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Id = message.Id
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}