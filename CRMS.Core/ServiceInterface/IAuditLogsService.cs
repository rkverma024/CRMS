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
        void CreateTicketComment();
        IEnumerable<AuditLogs> GetListOfAuditLogs();
    }
}
