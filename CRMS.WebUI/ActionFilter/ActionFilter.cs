using CRMS.Core.ViewModel;
using CRMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRMS.WebUI
{
    public class ActionFilter : ActionFilterAttribute
    {
        public string FormAccessCode;
        public readonly CheckRoleRights.FormAccessCode form;

        public ActionFilter(string _formAccessCode, CheckRoleRights.FormAccessCode Form)
        {
            form = Form;
            FormAccessCode = _formAccessCode;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool checkAccess = CheckAccess.ChechAccessPermission(FormAccessCode, form.ToString());
            if (!checkAccess)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Account" },
                    { "action","AccessDenied"},
                    { "returnUrl", HttpContext.Current.Request.Url}
                });
                throw new UnauthorizedAccessException();
            }
        }
    }
}