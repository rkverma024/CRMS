using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL.Repository
{
    public class AuditLogsRepository : IAuditLogsRepository
    {
        internal DataContext context;
        internal DbSet<AuditLogs> dbSet;
        public AuditLogsRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<AuditLogs>();
        }
        public void Insert(AuditLogs model)
        {
            dbSet.Add(model);
        }
        public void Commit()
        {
            context.SaveChanges();
        }

        public IEnumerable<AuditLogs> Collection()
        {
            return dbSet;
        }
    }
}
