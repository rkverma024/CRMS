﻿using CRMS.DataAccess.SQL;
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
    
    public class AccountController : Controller
    {
        SqlRepository repository= new SqlRepository();
        public  AccountController()
        {
           
        }
        // GET: Account       

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("");
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
           var log = repository.LoginRepository().Where(x => x.Email == model.Email && x.Password == model.Password);
            
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    if (log.Count() > 0)
                    {
                        return RedirectToAction("Index","Home");
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
