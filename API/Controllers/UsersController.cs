using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;

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
        public ActionResult<IEnumerable<AppUser>> index()
        {
            return _context.Users.ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<AppUser> index(int id)
        {
            return _context.Users.Find(id);
        }
        
    }
}