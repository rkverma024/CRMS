using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class RoleController : Controller
    {
        IRepository<Role> context;

        public RoleController(IRepository<Role> roleContext)
        {
            context = roleContext;
        }
        // GET: Role
        public ActionResult Index()
        {
            List<Role> roles = context.Collection().ToList();
            return View(roles);
        }

        /*public ActionResult Details(Guid Id)
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
        }*/

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
            //RoleViewModel viewModel = new RoleViewModel();
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
                    roleToEdit.RoleType = role.RoleType;
                    roleToEdit.RoleCode = role.RoleCode;
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
        }       
    }   
}