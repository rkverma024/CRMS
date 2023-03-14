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
        private IUserService userservice;
        private IRoleService roleservice;

        public UserManageController(IUserService userService, IRoleService roleService)
        {
            userservice = userService;
            roleservice = roleService;
           
        }

        // GET: UserManage
        public ActionResult Index()
        {
            List<User> user = userservice.GetUserList().ToList();
            return View(user);
        }
        public ActionResult Create()
        {
            UserViewModel user = new UserViewModel();
            user.RoleDropdown = roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();                
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                userservice.CreateUser(model);
                return RedirectToAction("Index");
            }
        }

      /*  public ActionResult Edit(Guid Id)
        {
            User user = userservice.GetUser(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                UserViewModel userModel = new UserViewModel();
                userModel.Name = user.Name;
                userModel.Email = user.Email;
                userModel.Password = user.Password;
                
                return View(userModel);
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
        }*/
    }
}