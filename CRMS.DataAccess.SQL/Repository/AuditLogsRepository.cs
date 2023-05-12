using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using CRMS.Core.ViewModel;
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

        public IEnumerable<AuditLogsIndexViewModel> GetAllAuditLogList()
        {
            var list = (from adt in context.AuditLog 
                        join us in context.Users on adt.UserId equals us.Id
                        orderby adt.ExecutionTime descending
                        select new AuditLogsIndexViewModel()
                        {
                            Id = adt.Id,
                            UserName = us.UserName,
                            ExecutionTime = adt.ExecutionTime.ToString(),
                            //ExecutionTime = adt.ExecutionTime,
                            ExecutionDuration = adt.ExecutionDuration,
                            ClientAddress = adt.ClientAddress,
                            BrowserInfo = adt.BrowserInfo,
                            HttpMethod = adt.HttpMethod,
                            Url = adt.Url,
                            HttpStatusCode = adt.HttpStatusCode,
                            Comments = adt.Comments,
                            Parameters = adt.Parameters
                        }).ToList();
            
            
            return list;
        }

        public AuditLogsIndexViewModel GetAuditLogDetailsById(Guid Id)
        {
            var record = (from adt in context.AuditLog
                        join us in context.Users on adt.UserId equals us.Id
                        where adt.Id == Id
                        select new AuditLogsIndexViewModel()
                        {
                            Id = adt.Id,
                            UserName = us.UserName,
                            //ExecutionTime = adt.ExecutionTime,
                            ExecutionTime = adt.ExecutionTime.ToString(),
                            ExecutionDuration = adt.ExecutionDuration,
                            ClientAddress = adt.ClientAddress,
                            BrowserInfo = adt.BrowserInfo,
                            HttpMethod = adt.HttpMethod,
                            Url = adt.Url,
                            HttpStatusCode = adt.HttpStatusCode,
                            Comments = adt.Comments,
                            Parameters = adt.Parameters
                        }).FirstOrDefault();
            return record;
        }
    }
}
