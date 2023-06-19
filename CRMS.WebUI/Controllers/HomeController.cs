using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS.Core.Models;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using CRMS.Services;
using CRMS.WebUI.AuditLogFilter;

namespace CRMS.WebUI.Controllers
{
    //[RoutePrefix("Account")]
    [Authorize]
    public class HomeController : Controller
    {
        private UserRoleService userRoleService;
        private FormRoleMappingService formRoleMappingService;
        private IDashBoardService DashBoardService;
        public HomeController(UserRoleService userroleService, FormRoleMappingService FormRoleMappingService, IDashBoardService DashBoardService)
        {
            userRoleService = userroleService;
            formRoleMappingService = FormRoleMappingService;
            this.DashBoardService = DashBoardService;
        }
        //[Route("Index")]
        [AuditLogsFilter()]
        [ActionFilter("SS", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {           
            ViewBag.activeTabId = TempData["FormName"] as string;
            //ViewBag.activeUrl = TempData["Url"] as string;            
            return View();
        }

        /*public ActionResult Index(int activeTabId = 0)
        {            
            ViewBag.activeTabId = activeTabId;
            return View();
        }*/


        /*DashBoard*/

        [AuditLogsFilter()]
        [ActionFilter("DB", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult DashBoard()
        {
            DashBoardViewModel viewModel = DashBoardService.GetTicketCount();

            DashBoardViewModel priorityList = DashBoardService.GetChart();
            var data = priorityList.ChartData.ToList();
            viewModel.Chart= data;
            ViewBag.ChartData = data;

            DashBoardViewModel typeList = DashBoardService.GetTypeChart();
            var datatype = typeList.TypeChartData.OrderByDescending(x => x.value).ToList();
            viewModel.TypeChart = datatype;
            ViewBag.TypeChartData = datatype;

            return View(viewModel);
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