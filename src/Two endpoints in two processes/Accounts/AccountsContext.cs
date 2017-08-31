namespace Accounts
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal Balance { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}