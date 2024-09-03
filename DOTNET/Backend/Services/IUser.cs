using Microsoft.AspNetCore.Mvc;
using v_conf_dn.Models;

namespace v_conf_dn.Services
{
    public interface IUser
    {
        Task<ActionResult<User>?> createUser(User user);
        Task<User?> AuthenticateAsync(string username, string password);
        Task<ActionResult<User>> getUSerId(String username);

    }
}
