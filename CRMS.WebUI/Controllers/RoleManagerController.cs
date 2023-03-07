using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class RoleManagerController : Controller
    {



        //public RoleManagerController()


        // GET: Role

        public ActionResult Index()
        {
            List<Roles> role = context.Collection().ToList();
            return View(role);
        }

        public ActionResult Create()
        {
            RoleManagerViewModel viewModel = new RoleManagerViewModel();

            viewModel.Roles = new Roles();
            return View("ViewModel");
        }
    }
}