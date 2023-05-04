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
    public class RoleRepository : IRoleRepository
    {
        internal DataContext context;
        internal DbSet<Role> dbSet;

        public RoleRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Role>();
        }
        public IQueryable<Role> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }
        public void Delete(Guid Id)
        {
            var role = Find(Id);
            if (context.Entry(role).State == EntityState.Detached)
            {
                dbSet.Attach(role);
            }
            dbSet.Remove(role);
        }
        public Role Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(Role role)
        {
            dbSet.Add(role);

        }
        public void Update(Role updateRole)
        {
            dbSet.Attach(updateRole);
            context.Entry(updateRole).State = EntityState.Modified;
        }
    }
}
