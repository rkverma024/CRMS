using CRMS.Core.Models;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using CRMS.WebUI.AuditLogFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class AuditLogsController : Controller
    {
        IAuditLogsService auditLogsService;

        public AuditLogsController(IAuditLogsService AuditLogsService)
        {
            auditLogsService = AuditLogsService;
        }
        // GET: AuditLogs

        [AuditLogsFilter()]
        [ActionFilter("ADL", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            IEnumerable<AuditLogs> auditLogs = auditLogsService.GetListOfAuditLogs().ToList();
            return View(auditLogs);
        }
    }
}