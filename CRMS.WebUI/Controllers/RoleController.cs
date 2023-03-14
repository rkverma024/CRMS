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
        private IRoleServiceRepository roleservice;

        public RoleController(IRoleServiceRepository roleService)
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {                               
                roleservice.CreateRole(model);
                return RedirectToAction("Index");
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
                    roleToEdit.RoleName = role.RoleName;
                    roleToEdit.Code = role.Code;
                    roleservice.UpdateRole(roleToEdit);

                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Delete(Guid Id)
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
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {

            Role roleToDelete = roleservice.GetRole(Id);
            if (roleToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                roleservice.RemoveRole(roleToDelete);                
                return RedirectToAction("Index");
            }            
        }









        /*IRoleRepository context;

        public RoleController(IRoleRepository roleContext)
        {
            context = roleContext;
        }
        // GET: Role
        public ActionResult Index()
        {
            //RoleViewModel viewModel = new RoleViewModel();
            List<Role> roles = context.Collection().ToList();
            return View(roles);
        }        

        public ActionResult Create()
        {
            RoleViewModel viewModel = new RoleViewModel();



            viewModel.Role = new Role();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            else
            {
                context.Insert(role);
                context.Commit();

                return RedirectToAction("Index");
            }            
        }
        public ActionResult Edit(Guid Id)
        {
           
            Role role = context.Find(Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(role);
            }
        }
        [HttpPost]
        public ActionResult Edit(Role role, Guid Id)
        {

            Role roleToEdit = context.Find(Id);

            if (role == null)
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
                    roleToEdit.RoleName = role.RoleName;
                    roleToEdit.Code = role.Code;
                    context.Update(roleToEdit);
                    context.Commit();
                    context.Collection();
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Delete(Guid Id)
        {
            Role role = context.Find(Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(role);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {
            Role roleToDelete = context.Find(Id);
            if (roleToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }       */
    }   
}