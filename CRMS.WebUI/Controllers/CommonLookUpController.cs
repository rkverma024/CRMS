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
    //[Authorize]
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
            return PartialView("_Create", commonlookup);
        }

        [HttpPost]
        public ActionResult Create(CommonLookUpViewModel model)
        {            
            if (!ModelState.IsValid)
            {
                return Content("False");
            }
            else
            {
                bool existingmodel = commonLookUpservice.IsExist(model, true);
                if (existingmodel)
                {
                    TempData["Already"] = "Already Data is exist";
                    return Content("exists");
                }
                else
                {
                    commonLookUpservice.CreateCommonLookUp(model);
                    TempData["AlertMessage"] = "Added Successfully..!";
                    return Content("true");                    
                }
            }            
        }

        public ActionResult Edit(Guid Id)
        {

            CommonLookUp commonLookUp = commonLookUpservice.GetCommonLookUp(Id);
            if (commonLookUp == null)
            {
                return HttpNotFound();
            }
            else
            {
                CommonLookUpViewModel commonLookUpModel = new CommonLookUpViewModel();
                commonLookUpModel.ConfigName = commonLookUp.ConfigName;
                commonLookUpModel.ConfigKey = commonLookUp.ConfigKey;
                commonLookUpModel.DisplayOrder = commonLookUp.DisplayOrder;
                commonLookUpModel.Description = commonLookUp.Description;
                commonLookUpModel.ConfigValue = commonLookUp.ConfigValue;
                commonLookUpModel.IsActive = commonLookUp.IsActive;

                return PartialView("_Edit", commonLookUpModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(CommonLookUpViewModel commonLookUp, Guid Id)
        {                       
                if (!ModelState.IsValid)
                {
                    return Content("False");                    
                }
                else
                {
                    bool existingmodel = commonLookUpservice.IsExist(commonLookUp, false);
                    if (existingmodel)
                        {
                            TempData["Already"] = "Alredy Data is exist";
                            return Content("exists");
                        }
                        else
                        {                        
                            commonLookUpservice.UpdateCommonLookUp(commonLookUp, Id);
                            TempData["AlertMessage"] = "Updated Successfully..!";
                            return Content("true");                        
                        }
                    }
            }        
        public ActionResult Delete(Guid Id)
        {
            CommonLookUp commonLookUpToDelete = commonLookUpservice.GetCommonLookUp(Id);
            commonLookUpservice.RemoveCommonLookUp(commonLookUpToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");
        }
    }
}