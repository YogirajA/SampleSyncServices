namespace SyncUsersEndpoints
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;

    public class RabbitMessageHandler : IHandleMessages<NewUser>
    {
        public async Task Handle(NewUser message, IMessageHandlerContext context)
        {
            using (var dbcontext = new AccountsContext())
            {
                var user = new User
                {
                    CreatedOn = message.CreatedOn,
                    ModifiedOn = message.ModifiedOn,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    Id = message.Id
                };
                dbcontext.Users.Add(user);
                await dbcontext.SaveChangesAsync();
            }
        }
    }

    public class AccountsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AccountsContext():base("AccountsAppDatabase")
        {
            
        }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}