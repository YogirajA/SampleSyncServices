namespace Accounts.Controllers
{
    using System;

    public class NewAccount    
    {
        public Guid UserId { get; set; }
        public decimal InitialDeposit { get; set; }
    }
}