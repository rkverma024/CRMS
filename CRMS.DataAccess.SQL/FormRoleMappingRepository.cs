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
    public class FormRoleMappingRepository : IFormRoleMappingRepository
    {
        internal DataContext context;
        internal DbSet<FormRoleMapping> dbSet;
        public FormRoleMappingRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<FormRoleMapping>();
        }
        public IQueryable<FormRoleMapping> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var formRolemst = Find(Id);
            if (context.Entry(formRolemst).State == EntityState.Detached)
            {
                dbSet.Attach(formRolemst);
            }
            dbSet.Remove(formRolemst);
        }

        public FormRoleMapping Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(FormRoleMapping formRoleMapping)
        {
            dbSet.Add(formRoleMapping);
        }

        public void Update(FormRoleMapping formRoleMapping)
        {
            dbSet.Attach(formRoleMapping);
            context.Entry(formRoleMapping).State = EntityState.Modified;
        }
    }
}
