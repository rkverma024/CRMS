using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ServiceInterface
{
    public interface IAuditLogsService
    {
        void CreateAuditLog(string error);
        IEnumerable<AuditLogs> GetListOfAuditLogs();
        IEnumerable<AuditLogsIndexViewModel> IndexOfAuditLogs();

        AuditLogsIndexViewModel AuditLogDetailsById(Guid Id);

        List<AuditLogs> IndexErrorLog();
    }
}
