using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.DataAccess.SQL;
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
        LoginRepository repository = new LoginRepository();

        public AccountController()
        {

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
            ScryptEncoder encoder = new ScryptEncoder();
           
            if (!ModelState.IsValid)    
            {

                return View(model);
            }
            else
            {                               

                User user = repository.Login(model.Email);
                bool isValidUser = encoder.Compare(model.Password, user.Password);
                if (isValidUser)
                {
                    TempData["AlertMessage"] = "Login Successfully..!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Incorrect Email OR Password " ;                    
                    return View();
                }
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
