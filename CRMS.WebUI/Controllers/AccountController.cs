using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.DataAccess.SQL;
using CRMS.Services;
using CRMS.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Core.Types;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRMS.WebUI.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private LoginService loginService;
        private UserService userservice;
        //LoginRepository repository = new LoginRepository();

        public AccountController(LoginService LoginService, UserService userService)
        {
            loginService = LoginService;
            userservice = userService;
        }
       
        // GET: Account       

        //[AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            
            return View("");
        }
        [HttpPost]
        //[AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {          
            if (!ModelState.IsValid)    
            {
                return View(model);
            }
            else
            {
                User user = loginService.Login(model);
                if (user != null)
                {
                    ScryptEncoder encoder = new ScryptEncoder();
                    bool isValidUser = encoder.Compare(model.Password, user.Password);
                    if (isValidUser)
                    {
                        Session["Email"] = user.Email;
                        Session["UserName"] = user.UserName;
                        Session["Id"] = user.Id;
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        TempData["AlertMessage"] = "Login Successfully..!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Message"] = "Incorrect Email OR Password ";
                        return View();
                    }
                }
                else
                {
                    TempData["Message"] = "Incorrect Email OR Password ";
                    return View();
                }

            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();            
            return RedirectToAction("Login");
        }
    }
}
