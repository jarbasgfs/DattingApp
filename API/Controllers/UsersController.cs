using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: DefaultController
    {

        public UsersController(DataContext context): base(context)
        {
            
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> index()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> index(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        
    }
}