using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRI7_DataAccess.Data;
using LRI7_DataAccess.Repository;
using LRI7_DataAccess.Repository.IRepository;
using LRI7_DataAccess.UnitOfWork.IUnitOfwork;

namespace LRI7_DataAccess.UnitOfWork
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly DatabaseContext _db;
        public IAccountRepository Account { get; private set; }
        public UnitOfWork(DatabaseContext db)
        {
            _db = db;
            Account = new AccountRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
