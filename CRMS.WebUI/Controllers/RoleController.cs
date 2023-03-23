using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    //[Authorize]
    public class RoleController : Controller
    {
        private IRoleService roleservice;

        public RoleController(IRoleService roleService)
        {
            roleservice = roleService;
        }
        /*public RoleController()
        {

        }*/
        // GET: RoleManagement
        public ActionResult Index()
        {
            List<Role> roles = roleservice.GetRolesList().ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            RoleViewModel roles = new RoleViewModel();
            return View(roles);
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            bool existingmodel = roleservice.IsExist(model, true);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (existingmodel)
                {
                    TempData["Already"] = "Already Data is exist";
                    return View();
                }
                else
                {
                    roleservice.CreateRole(model);
                    TempData["AlertMessage"] = "Added Successfully..!";                 
                    return RedirectToAction("Index");
                }
                
            }
        }
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
            bool existingmodel = roleservice.IsExist(role, false);
            Role roleToEdit = roleservice.GetRole(Id);

            if (roleToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    if (existingmodel)
                    {
                        TempData["Already"] = "Alredy Data is exist";
                        return View();

                    }
                    else
                    {
                        roleToEdit.RoleName = role.RoleName;
                        roleToEdit.Code = role.Code;
                        roleservice.UpdateRole(roleToEdit);
                        TempData["AlertMessage"] = "Updated Successfully..!";
                        return RedirectToAction("Index");
                    }
                    
                }
            }
        }
        /*public ActionResult Delete(Guid Id)
        {
            Role role = roleservice.GetRole(Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(role);
            }
        }*/

       /* [HttpPost]
        [ActionName("Delete")]*/
        public ActionResult Delete(Guid Id)
        {

            Role roleToDelete = roleservice.GetRole(Id);
            roleservice.RemoveRole(roleToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");
/*
            if (roleToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                roleservice.RemoveRole(roleToDelete);                
                return RedirectToAction("Index");
            }            */
        }        
    }   
}