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
    public class FormMstRepository : IFormMstRepository
    {
        internal DataContext context;
        internal DbSet<FormMst> dbSet;

        public FormMstRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<FormMst>();
        }
        public IQueryable<FormMst> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var formmst = Find(Id);
            if (context.Entry(formmst).State == EntityState.Detached)
            {
                dbSet.Attach(formmst);
            }
            dbSet.Remove(formmst);
        }

        public FormMst Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(FormMst formMst)
        {
            dbSet.Add(formMst);
        }

        public void Update(FormMst formMst)
        {
            dbSet.Attach(formMst);
            context.Entry(formMst).State = EntityState.Modified;
        }
    }
}
