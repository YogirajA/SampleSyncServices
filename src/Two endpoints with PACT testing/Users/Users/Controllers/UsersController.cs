namespace Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public Task<List<User>> All()
        {
            return _context.Users.ToListAsync();
        }

        [Route("{id:guid}")]
        public Task<User> Get(Guid id)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        [HttpPost("new")]
        public async Task<Guid> AddNew([FromBody]NewUser newUser)
        {
            var user = new User
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
