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
    public class UserRepository : IUserRepository
    {
        internal DataContext context;
        internal DbSet<User> dbSet;

        public UserRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<User>();
        }

        public IQueryable<User> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var user = Find(Id);
            if (context.Entry(user).State == EntityState.Detached)
            {
                dbSet.Attach(user);
            }
            dbSet.Remove(user);
        }

        public User Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(User user)
        {
            dbSet.Add(user);
        }
        public void Update(User updateUser)
        {
            dbSet.Attach(updateUser);
            context.Entry(updateUser).State = EntityState.Modified;
        }
    }
}
