using Microsoft.AspNetCore.Identity;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            var user = await _userHelper.GetUserByEmailAsync("catiasantos0202@gmail.com");
            if(user == null)
            {
                user = new User
                {
                    FirstName = "catias",
                    LastName = "santos",
                    Email = "catiasantos0202@gmail.com",
                    UserName="catiasantos0202@gmail.com",
                    Password="123456"
                

                };
                var result = await _userHelper.AddUserAsync(user, "123456");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

        }

    }
}

