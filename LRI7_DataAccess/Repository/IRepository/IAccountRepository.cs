using LRI7_DataAccess.Entity;
using LRI7_Models;
using LRI7_Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRI7_DataAccess.Repository.IRepository
{
    public interface IAccountRepository:IRepository<ApplicationUser>
    {
        Task<Status> LoginAsync(Login user);

        Task<Status> RegisterAsync(Registration user);

        Task LogoutAsync();

    }
}
