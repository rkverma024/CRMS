using CRMS.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRMS.WebUI.AuditLogFilter
{
    public class AuditLogsFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var logs = DependencyResolver.Current.GetService<IAuditLogsService>();
            logs.CreateAuditLog(null);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var logs = DependencyResolver.Current.GetService<IAuditLogsService>();
            if (filterContext.Exception != null)
            {
                logs.CreateAuditLog(filterContext.Exception.Message);
            }
        }
    }
}