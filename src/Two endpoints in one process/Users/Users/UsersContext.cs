namespace Users
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options) { }

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
}