namespace Accounts.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly AccountsContext _context;
        public AccountsController(AccountsContext context)
        {
            _context = context;
        }
        [Route("{id:guid}")]
        public Task<Account> Get(Guid id)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        }
        [Route("foruser/{userId:guid}")]
        public Task<Account> ForUser(Guid userId)
        {
            return _context.Accounts.Include(x=>x.User).FirstOrDefaultAsync(x => x.UserId == userId);
        }
        //post for user
    }
}