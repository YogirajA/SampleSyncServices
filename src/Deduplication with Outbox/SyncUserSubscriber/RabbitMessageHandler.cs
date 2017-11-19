namespace SyncUserSubscriber
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;
    using Messages;
    using Messages.V1;
    using NServiceBus;

    public class RabbitMessageHandler : IHandleMessages<NewUser>
    {
        private readonly AccountsContext _context;

        public RabbitMessageHandler(AccountsContext context)
        {
            _context = context;
        }
        public Task Handle(NewUser message, IMessageHandlerContext context)
        {
            // I am making deliberate decisions for handling the null values in my handler
            if (!message.Id.HasValue)
                throw new Exception("The record can't be synced because the Id is null");

            var sqlDateTime = Convert.ToDateTime(SqlDateTime.MinValue);

            var user = new User
            {
                CreatedOn = message.CreatedOn.GetValueOrDefault(sqlDateTime),
                ModifiedOn = message.ModifiedOn.GetValueOrDefault(sqlDateTime),
                FirstName = message.FirstName,
                LastName = message.LastName,
                Id = message.Id.Value
            };
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }
    }
}