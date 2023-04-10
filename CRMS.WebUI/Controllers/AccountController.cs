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
using System.Web.Routing;
using System.Web.Security;

namespace CRMS.WebUI.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private LoginService loginService;
        private IUserService userservice;
        private IFormMstService formMstService;

        //LoginRepository repository = new LoginRepository();

        public AccountController(LoginService LoginService, UserService userService, IFormMstService FormMstService)
        {
            loginService = LoginService;
            userservice = userService;
            formMstService = FormMstService;
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

                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        Session["Email"] = user.Email;
                        Session["UserName"] = user.UserName;
                        Session["Name"] = user.Name;
                        Session["Id"] = user.Id;                        
                        IEnumerable<FormMstViewModel> formList = formMstService.GetFormMstsIndexList();
                        Session["FormLists"] = formList;
                        /*TempData["AlertMessage"] = "Login Successfully..!";*/
                        /* return RedirectToAction("Index", "Home");*/
                        return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index" }));
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
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        /*   public ActionResult ForgotPassword()
           {
               return ();
           }*/
    }
}
