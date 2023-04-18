using LRI7_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRI7_DataAccess.UnitOfWork.IUnitOfwork
{
    public interface IunitOfWork
    {
        IAccountRepository Account { get; }

        void Save();
       
    }
}
