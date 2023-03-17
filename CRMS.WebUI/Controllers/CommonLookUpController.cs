using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class CommonLookUpController : Controller
    {
        private ICommonLookUpService commonLookUpservice;

        public CommonLookUpController(ICommonLookUpService CommonLookUpservice)
        {
            commonLookUpservice = CommonLookUpservice;
        }

        // GET: CommanLookUp
        public ActionResult Index()
        {
            List<CommonLookUp> commonLookUp = commonLookUpservice.GetCommonLookUpsList().ToList();
            return View(commonLookUp);
        }
    }
}