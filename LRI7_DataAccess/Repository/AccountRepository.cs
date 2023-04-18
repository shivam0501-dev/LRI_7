using LRI7_DataAccess.Data;
using LRI7_DataAccess.Entity;
using LRI7_DataAccess.Repository.IRepository;
using LRI7_Models;
using LRI7_Utility.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LRI7_DataAccess.Repository
{
    public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
    {
        private readonly DatabaseContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountRepository(DatabaseContext db):base(db)
        {
            _db= db;
        }

        public async Task<Status> Registration(ApplicationUser entity)
        {
            Status s = new();
            var userExists=_userManager.FindByNameAsync(entity.UserName);
            return s;
        }

        
    }
}
