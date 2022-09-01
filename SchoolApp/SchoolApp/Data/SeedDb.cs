using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        /*private readonly IUserHelper _userHelper*/
        private Random _random;
        public SeedDb(DataContext context/*, IUserHelper userHelper*/)
        {
            _context = context;
            //_userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}

