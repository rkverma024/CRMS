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
            var list = userservice.GetUserRoleList();
            return View(list);           
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
                model.RoleDropdown = roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
                return View(model);
            }
            else 
            {
                bool existingmodel = userservice.IsExist(model, true);
                if (existingmodel)
                {
                    TempData["Already"] = "Email is Already exist";
                    model.RoleDropdown = roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
                    return View(model);
                }
                else
                {
                    userservice.CreateUser(model);
                    TempData["AlertMessage"] = "Added Successfully..!";
                    return RedirectToAction("Index");
                }               
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
                userModel.UserName = user.UserName;
                userModel.Gender = user.Gender;
                userModel.MobileNo = user.MobileNo;
                userModel.Role = _useroleservice.GetUserRole(user.Id).RoleId; ;
                userModel.RoleDropdown= roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
                return View(userModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model, Guid Id)
        {            
            if (!ModelState.IsValid)
           {
               return View();
           }
           else
           {
                bool existingmodel = userservice.IsExist(model, false);
                if (existingmodel)
                {
                    TempData["Already"] = "Email is Already exist";
                    model.RoleDropdown = roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
                    return View(model);
                }
                else
                {
                    userservice.UpdateUser(model, Id);
                    TempData["AlertMessage"] = "Updated Successfully..!";
                    return RedirectToAction("Index");
                }
                             
           }
        }       
        public ActionResult Delete(Guid Id)
        {
            User userToDelete = userservice.GetUserById(Id);
            userservice.RemoveUser(userToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");           
        }
    }
}