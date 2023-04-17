using CRMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{

    public class BaseController : Controller
    {
        private FormRoleMappingService formRoleMappingService;
        public BaseController(FormRoleMappingService formroleMappingService)
        {
            formRoleMappingService = formroleMappingService;
        }
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
        /*public bool CheckPermission(string formCode, string formAction)
        {
            var checkPermission = formRoleMappingService.;
            if (checkPermission != null)
            {
                
            }
            return false;
        }*/
    }
}