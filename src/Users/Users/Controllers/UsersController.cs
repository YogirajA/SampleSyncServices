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
        // GET api/values
        [HttpGet("all")]
        public async Task<List<User>> All()
        {
            return await _context.Users.ToListAsync();
        }
        
        [Route("{id:guid}")]
        public async Task<User> Get(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        // POST api/values
        [HttpPost("Add")]
        public async Task<Guid> Add([FromBody]UserModel userModel)
        {
            var user = new User
            {
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                FirstName = userModel.FirstName,
                LastName = userModel.LastName
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }

    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
