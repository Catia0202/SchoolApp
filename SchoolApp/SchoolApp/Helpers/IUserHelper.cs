using Microsoft.AspNetCore.Identity;
using SchoolApp.Data.Entities;
using System.Threading.Tasks;

namespace SchoolApp.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string passwords);
    }
}
