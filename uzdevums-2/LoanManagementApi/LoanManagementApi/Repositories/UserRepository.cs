using LoanManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ManagementDBContext _ctx;

        public UserRepository(ManagementDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> CreateUser(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(user => user.UserId == id);
        }
    }
}
