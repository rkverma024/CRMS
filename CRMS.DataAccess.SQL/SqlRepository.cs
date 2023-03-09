using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {

        internal DataContext context;
        internal DbSet<T> dbSet;

        //public bool IsDeleted { get; set; }

        public SqlRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }
            dbSet.Remove(t);
        }

        public T Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        //public T Find(Guid Id)
        //{
        //    throw new NotImplementedException();
        //}

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }        
    }
}
/*internal DataContext context;


public SqlRepository()
{

    //this.context = context;
    context = new DataContext();
}
public IQueryable<User> LoginRepository()
{
    var user = context.Users.Where(x => x.Role == "Admin");
    return user;
}*/