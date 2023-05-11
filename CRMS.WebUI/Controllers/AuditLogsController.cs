using CRMS.Core.Models;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using CRMS.WebUI.AuditLogFilter;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
            IEnumerable<AuditLogsIndexViewModel> auditLogs = auditLogsService.IndexOfAuditLogs().ToList();
            return PartialView("_AuditLogView", auditLogs);
        }

        [AuditLogsFilter()]
        public JsonResult AuditLogGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<AuditLogsIndexViewModel> auditLogs = auditLogsService.IndexOfAuditLogs().ToList();
            return Json(auditLogs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid Id)
        {
            AuditLogsIndexViewModel model = auditLogsService.AuditLogDetailsById(Id);
            return View(model);
        }
    }
}