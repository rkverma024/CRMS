using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class CommanLookUpController : Controller
    {
        private ICommanLookUpService commanLookUpservice;

        public CommanLookUpController(ICommanLookUpService CommanLookUpservice)
        {
            commanLookUpservice = CommanLookUpservice;
        }

        // GET: CommanLookUp
        public ActionResult Index()
        {
            List<CommanLookUp> commanLookUp = commanLookUpservice.GetCommanLookUpsList().ToList();
            return View(commanLookUp);
        }
    }
}