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
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            List<Role> roles = roleservice.GetRolesList().ToList();
            return PartialView("_RoleListLayout", roles);
            /*return View(roles);*/
        }
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            RoleViewModel roles = new RoleViewModel();
            return View(roles);
        }

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
                    /*return RedirectToAction("Index","Home");*/
                    return new RedirectResult(Url.Action("Index", "Home", new { activeTabId = 1 }));
                    //return Redirect(Request.UrlReferrer.ToString());
                }
            }
        }
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsEdit)]
        public ActionResult Edit(Guid Id)
        {
            Role role = roleservice.GetRole(Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                RoleViewModel roleModel = new RoleViewModel();
                roleModel.RoleName = role.RoleName;
                roleModel.Code = role.Code;
                return View(roleModel);
            }
        }

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
                    /*return RedirectToAction("Index","Home");*/
                    return new RedirectResult(Url.Action("Index", "Home", new { activeTabId = 1 }));
                }
            }
        }

        /* [HttpPost]
         [ActionName("Delete")]*/
        [ActionFilter("RL", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            Role roleToDelete = roleservice.GetRole(Id);
            roleservice.RemoveRole(roleToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            /*return RedirectToAction("Index", "Home");*/
            return new RedirectResult(Url.Action("Index", "Home", new { activeTabId = 1 }));
        }
        public JsonResult RoleGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Role> roles = roleservice.GetRolesList().ToList();
            return Json(roles.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }        

    }   
}