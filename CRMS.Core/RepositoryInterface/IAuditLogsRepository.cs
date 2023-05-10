using CRMS.Core.Models;
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
    }
}
