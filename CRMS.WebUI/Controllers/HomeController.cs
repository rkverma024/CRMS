using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS.Core.ViewModel;
using CRMS.Services;
namespace CRMS.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [ActionFilter("SS", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index(int activeTabId = 0)
        {
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