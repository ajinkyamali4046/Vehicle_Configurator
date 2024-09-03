using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Repository;

namespace v_conf_dn.Services
{
    public class UserService : IUser
    {
        private readonly VehicleDBContext _dbContext;

        public UserService(VehicleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<User>?> createUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return (user);
        }





        public async Task<ActionResult<User>> getUSerId(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            // Retrieve the user by username
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return null; // User not found
            }

            // Check the password (you should hash and compare hashed passwords in production)
            if (user.Password == password)
            {
                return user; // Authentication successful
            }

            return null; // Incorrect password
        }
    }
}
