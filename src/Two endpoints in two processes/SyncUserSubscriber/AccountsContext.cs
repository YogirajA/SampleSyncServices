namespace SyncUserSubscriber
{
    using System;
    using System.Data.Entity;

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