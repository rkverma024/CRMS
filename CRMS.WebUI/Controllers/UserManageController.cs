using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CRMS.WebUI.Controllers
{
    [Authorize]
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

        [ActionFilter("US", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {            
            var list = userservice.GetUserRoleList().OrderBy(x => x.Role);
            return View(list);           
        }

        [ActionFilter("US", CheckRoleRights.FormAccessCode.IsInsert)]
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
                    model.CreatedBy = (Guid)Session["Id"];
                    userservice.CreateUser(model);                    
                    TempData["AlertMessage"] = "Added Successfully..!";
                    //TempData["FormName"] = "User";
                    //return RedirectToAction("Index");
                    return new RedirectResult(Url.Action("Index", "Home"));
                }               
            }
        }
        [ActionFilter("US", CheckRoleRights.FormAccessCode.IsEdit)]
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
                model.RoleDropdown = roleservice.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
                return View(model);
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
                    //TempData["FormName"] = "User";
                    // return RedirectToAction("Index");
                    return new RedirectResult(Url.Action("Index", "Home"));
                }
                             
           }
        }
        [ActionFilter("US", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            User userToDelete = userservice.GetUserById(Id);
            userservice.RemoveUser(userToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            //TempData["FormName"] = "User";
            return RedirectToAction("Index");           
        }

        public JsonResult UserGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<IndexViewModel> list = userservice.GetUserRoleList().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}