using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.Services;
namespace CRMS.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserRoleService userRoleService;
        private FormRoleMappingService formRoleMappingService;
        public HomeController(UserRoleService userroleService, FormRoleMappingService FormRoleMappingService)
        {
            userRoleService = userroleService;
            formRoleMappingService = FormRoleMappingService;
        }
        [ActionFilter("SS", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index(int activeTabId = 0)
        {
           /* var userId = (Guid)Session["Id"];
            var loginRoleId = userRoleService.GetUserRoleList().Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefault();
            IEnumerable<FormRoleMapping> formRoleMappings = formRoleMappingService.GetList().Where(x => x.RoleId == loginRoleId).ToList();
            Session["Permission"] = formRoleMappings;*/
            ViewBag.activeTabId = activeTabId;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}