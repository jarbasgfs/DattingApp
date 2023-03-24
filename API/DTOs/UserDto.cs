using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Services;
using API.Interfaces;

namespace API.DTOs
{
    public class UserDto
    {
        public string Username {get; set;}
        public string Token { get; set; }
    }
}