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
    public class UserRoleRepository : IUserRoleRepository
    {
        internal DataContext context;
        internal DbSet<UserRole> dbSet;

        public UserRoleRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<UserRole>();
        }

        public IQueryable<UserRole> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var userRole = Find(Id);
            if (context.Entry(userRole).State == EntityState.Detached)
            {
                dbSet.Attach(userRole);
            }
            dbSet.Remove(userRole);
        }

        public UserRole Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(UserRole userRole)
        {
            dbSet.Add(userRole);
        }

        public void Update(UserRole updateUserRole)
        {
            dbSet.Attach(updateUserRole);
            context.Entry(updateUserRole).State = EntityState.Modified;
        }
    }
}
