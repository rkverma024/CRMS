using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.DataAccess.SQL;
using CRMS.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Core.Types;
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

        //IRepository repository = new IRepository();
        //private SqlRepository<User> repository;
        //public  AccountController(SqlRepository<User> repository)
        //{
        //    this.repository = repository;
        //}
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
        public ActionResult Login(User model, string returnUrl)
        {
            int user = repository.Login(model) ;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (user > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "UserName or password is wrong";
                    return View();
                }                
            }
        }
    }
}
