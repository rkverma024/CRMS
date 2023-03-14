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
    //[Authorize]
    public class UserManageController : Controller
    {
        IUserRepository context;
        IRoleRepository role;
      
        public UserManageController(IUserRepository usercontext, IRoleRepository rolecontext)
        {
            context = usercontext;
            role = rolecontext;
        }

        // GET: UserManage
        public ActionResult Index()
        {
            List<User> users = context.Collection().ToList();
            return View(users);
        }
        public ActionResult Create()
        {
            UserViewModel userViewModel = new UserViewModel();           
            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                context.Insert(user);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(Guid Id)
        {
            User user = context.Find(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                UserViewModel viewModel = new UserViewModel();
                viewModel.User = user;                
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult Edit(User user, Guid Id)
        {
            User userToEdit = context.Find(Id);
            if (user == null)
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
                    userToEdit.Name = user.Name;                    
                    userToEdit.Email = user.Email;
                    userToEdit.Password = user.Password;                   

                    context.Update(userToEdit);
                    context.Commit();
                    context.Collection();
                    return RedirectToAction("Index");                    
                }
            }
        }
        public ActionResult Delete(Guid Id)
        {
            User user = context.Find(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {
            User userToDelete = context.Find(Id);
            if (userToDelete == null)
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