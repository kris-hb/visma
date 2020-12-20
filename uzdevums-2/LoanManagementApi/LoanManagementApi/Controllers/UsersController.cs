using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementApi.Models;
using LoanManagementApi.Repositories;

namespace LoanManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;

        public UsersController(IUserRepository users)
        {
            _users = users;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _users.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _users.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            return await _users.CreateUser(user);

        }
    }
}
