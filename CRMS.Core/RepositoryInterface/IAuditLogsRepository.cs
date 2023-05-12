using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.RepositoryInterface
{
    public interface IAuditLogsRepository
    {
        void Insert(AuditLogs model);
        void Commit();
        IEnumerable<AuditLogs> Collection();
        IEnumerable<AuditLogsIndexViewModel> GetAllAuditLogList();
        AuditLogsIndexViewModel GetAuditLogDetailsById(Guid Id);

        //IEnumerable<AuditLogsIndexViewModel> GetErrorLogList();
    }
}
