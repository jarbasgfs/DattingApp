using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;


namespace API.Controllers
{
    public class DefaultController: ControllerBase
    {

        protected readonly DataContext _context;

        public DefaultController(DataContext context)
        {
            _context = context;
        }
        
    }
}