using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc;
using CRMS.WebUI.AuditLogFilter;

namespace CRMS.WebUI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private IRoleService roleservice;

        public RoleController(IRoleService roleService)
        {
            roleservice = roleService;
        }
        
        // GET: RoleManagement
        [AuditLogsFilter()]
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            List<Role> roles = roleservice.GetRolesList().ToList();
            return PartialView("_RoleListLayout", roles);
            /*return View(roles);*/
        }

        [AuditLogsFilter()]
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            RoleViewModel roles = new RoleViewModel();
            return View(roles);
        }

        [AuditLogsFilter()]
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                bool existingmodel = roleservice.IsExist(model, true);
                if (existingmodel)
                {
                    TempData["Already"] = "Already Data is exist.";
                    return View();
                }
                else
                {
                    model.CreatedBy = (Guid)Session["Id"];
                    roleservice.CreateRole(model);
                    TempData["AlertMessage"] = "Added Successfully..!";
                    //TempData["FormName"] = "Role";
                    return RedirectToAction("Index", "Home");
                    //return new RedirectResult(Url.Action("Index", "Home"));
                    //return Redirect(Request.UrlReferrer.ToString());
                }
            }
        }

        [AuditLogsFilter()]
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsEdit)]
        public ActionResult Edit(Guid Id)
        {
            Role obj = roleservice.GetRole(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var roleViewModel = roleservice.BindRoleVW(obj);
                return View(roleViewModel);
            }
        }

        [AuditLogsFilter()]
        [HttpPost]
        public ActionResult Edit(RoleViewModel role, Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool existingmodel = roleservice.IsExist(role, false);
                if (existingmodel)
                {
                    TempData["Already"] = "Alredy Data is exist";
                    return View();
                }
                else
                {
                    role.UpdatedBy = (Guid)Session["Id"];
                    roleservice.UpdateRole(role, Id);
                    TempData["AlertMessage"] = "Updated Successfully..!";
                    //TempData["FormName"] = "Role";
                    return RedirectToAction("Index", "Home");
                    //return new RedirectResult(Url.Action("Index", "Home"));
                }
            }
        }

        [AuditLogsFilter()]
        /* [HttpPost]
         [ActionName("Delete")]*/
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            Role roleToDelete = roleservice.GetRole(Id);
            roleservice.RemoveRole(roleToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            //TempData["FormName"] = "Role";
            return RedirectToAction("Index", "Home");
            //return new RedirectResult(Url.Action("Index", "Home"));
        }

        [AuditLogsFilter()]
        public JsonResult RoleGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Role> roles = roleservice.GetRolesList().ToList();
            return Json(roles.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}