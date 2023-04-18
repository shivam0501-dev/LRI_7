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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LRI7_DataAccess.Repository
{
    public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
    {
        private readonly DatabaseContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountRepository(DatabaseContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager) :base(db)
        {
            _db= db;
            _userManager= userManager;
            _roleManager= roleManager;
            _signInManager= signInManager;
        }

        public async Task<Status> LoginAsync(Login m)
        {
            Status s = new();
            var user =await _userManager.FindByNameAsync(m.UserName);
            if (user==null)
            {
                s.MessageCode = 0;
                s.Message = "Invalid UserName";
                return s;
            }
            if (!await _userManager.CheckPasswordAsync(user, m.Password))
            {
                s.MessageCode = 0;
                s.Message = "Invalid Password";
                return s;
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, m.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles=await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                foreach (var urole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, urole));
                }
                s.Message = "Logged in succesfully";
                s.MessageCode=1;
                return s;
            }
            else if (signInResult.IsLockedOut)
            {
                s.MessageCode = 0;
                s.Message = "User Locked Out";
                return s;
            }
            else
            {
                s.MessageCode= 0;
                s.Message = "Invalid UserName";
                return s;
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> RegisterAsync(Registration m)
        {
            Status s = new();
            var userExists = await _userManager.FindByNameAsync(m.UserName);
            if (userExists != null)
            {
                s.MessageCode = 0;
                s.Message = "User Already Exists";
                return s;
            }
            else
            {
                ApplicationUser u = new ApplicationUser()
                {
                    name=m.Name,
                    UserName=m.UserName,
                    Email=m.Email,
                    EmailConfirmed=true,
                    SecurityStamp=Guid.NewGuid().ToString()
                };

                var result= await _userManager.CreateAsync(u,m.Password);
                if(!result.Succeeded)
                {
                    s.MessageCode = 0;
                    s.Message = "User Creation Failed";
                    return s;
                }
                //role management 
                if(!await _roleManager.RoleExistsAsync(m.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(m.Role));
                }
                if(await _roleManager.RoleExistsAsync(m.Role))
                {
                    await _userManager.AddToRoleAsync(u, m.Role);
                }
                s.MessageCode=1;
                s.Message = "User Register Successfully";
                return s;
            }
        }
    }
}
