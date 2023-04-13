using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class FormRoleMappingController : Controller
    {
        private IFormRoleMappingService formRoleMappingService;
        public FormRoleMappingController(IFormRoleMappingService formroleMappingService)
        {
            formRoleMappingService = formroleMappingService;
        }

        // GET: FormRoleMapping
        public ActionResult Index(Guid? Id)
        {
            IEnumerable<FormRoleMappingViewModel> formRoleService = formRoleMappingService.GetFormRoleRights(Id);
            return View(formRoleService);
        }
        [HttpPost]
        public ActionResult FormRights(IEnumerable<FormRoleMapping> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                formRoleMappingService.AddFormRights(model);
                TempData["AlertMessage"] = "Permission Save Successfully..!";
                return Content("true");

            }
        }
    }
}