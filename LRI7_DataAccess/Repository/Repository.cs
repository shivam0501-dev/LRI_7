﻿using LRI7_DataAccess.Data;
using LRI7_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LRI7_DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _db;
        internal DbSet<T> dbSet;
        public Repository(DatabaseContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();
        }

        public void add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query= query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> query = dbSet;
            return query.ToList();
        }

        public void remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entites)
        {
           dbSet.RemoveRange(entites);
        }
    }
}
