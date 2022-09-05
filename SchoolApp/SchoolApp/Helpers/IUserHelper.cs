using Microsoft.AspNetCore.Identity;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Threading.Tasks;

namespace SchoolApp.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string passwords);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
