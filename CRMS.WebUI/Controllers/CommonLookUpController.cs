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
    public class CommonLookUpController : Controller
    {
        private ICommonLookUpService commonLookUpservice;

        public CommonLookUpController(ICommonLookUpService CommonLookUpservice)
        {
            commonLookUpservice = CommonLookUpservice;
        }

        // GET: CommonLookUp
        public ActionResult Index()
        {
            List<CommonLookUp> commonLookUp = commonLookUpservice.GetCommonLookUpsList().ToList();
            return View(commonLookUp);
        }

        public ActionResult Create()
        {
            CommonLookUpViewModel commonlookup = new CommonLookUpViewModel();
            return View(commonlookup);
        }

        [HttpPost]
        public ActionResult Create(CommonLookUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                commonLookUpservice.CreateCommonLookUp(model);
                //TempData["AlertMessage"] = "Role Added Successfully..!";
                return RedirectToAction("Index");
            }
        }
    }
}