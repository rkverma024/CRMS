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
        private IDashBordService dashBordService;
        public HomeController(UserRoleService userroleService, FormRoleMappingService FormRoleMappingService, IDashBordService dashBordService)
        {
            userRoleService = userroleService;
            formRoleMappingService = FormRoleMappingService;
            this.dashBordService = dashBordService;
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


        /*Dashbord*/

        [AuditLogsFilter()]
        [ActionFilter("DB", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Dashbord()
        {
            DashBordViewModel viewModel = dashBordService.GetTicketCount();

            DashBordViewModel priorityList = dashBordService.GetChart();
            var data = priorityList.ChartData.ToList();
            viewModel.Chart= data;
            ViewBag.ChartData = data;

            DashBordViewModel typeList = dashBordService.GetTypeChart();
            var datatype = typeList.TypeChartData.ToList();
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