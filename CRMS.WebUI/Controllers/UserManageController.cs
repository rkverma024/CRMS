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
        private IUserRoleService _useroleservice;

        public UserManageController(IUserService userService, IRoleService roleService, IUserRoleService useroleservice)
        {
            userservice = userService;
            roleservice = roleService;
            _useroleservice = useroleservice;
        }

        // GET: UserManage
        public ActionResult Index()
        {
            //List<User> user = userservice.GetUserList().ToList();
            var list = userservice.GetUserRoleList();
            return View(list);

            /*List<User> user = userservice.GetUserList().ToList();
            return View(user);*/
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

        public ActionResult Edit(Guid Id)
        {            
            User user = userservice.GetUserById(Id);            
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
                userModel.RoleId = _useroleservice.GetUserRole(user.Id).RoleId; ;
                userModel.RoleDropdown= roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
                return View(userModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user, Guid Id)
        {           
           if (!ModelState.IsValid)
           {
               return View();
           }
           else
           {                    
               userservice.UpdateUser(user, Id);                   
               return RedirectToAction("Index");                
           }
        }
        public ActionResult Delete(Guid Id)
        {
            User user = userservice.GetUserById(Id);
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
            User userToDelete = userservice.GetUserById(Id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                userservice.RemoveUser(userToDelete);
                return RedirectToAction("Index");
            }
        }
    }
}