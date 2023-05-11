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
    public class ErrorLogsController : Controller
    {

        IAuditLogsService auditLogsService;

        public ErrorLogsController(IAuditLogsService AuditLogsService)
        {
            auditLogsService = AuditLogsService;
        }
        // GET: ErrorLogs

        [AuditLogsFilter()]
        [ActionFilter("EE", CheckRoleRights.FormAccessCode.IsView)]        
        public ActionResult Index()
        {
            List<AuditLogs> errorLogs = auditLogsService.IndexErrorLog().ToList();
            return PartialView("_ErrorLogs", errorLogs);
        }

        [AuditLogsFilter()]
        public JsonResult ErrorLogGrid([DataSourceRequest] DataSourceRequest request)
        {
            List<AuditLogs> errorLogs = auditLogsService.IndexErrorLog().ToList();
            return Json(errorLogs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}