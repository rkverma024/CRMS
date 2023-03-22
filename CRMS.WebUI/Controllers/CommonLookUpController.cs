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
            return PartialView("_Create",commonlookup);
        }

        [HttpPost]
        public ActionResult Create(CommonLookUpViewModel model)
        {
            var existingmodel = commonLookUpservice.GetCommonLookUpsList().Where(x => x.ConfigKey == model.ConfigKey && x.ConfigName == model.ConfigName).Count();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {

                if (existingmodel > 0)
                {
                    TempData["Already"] = "Alredy Data is exist";

                }
                else
                {
                    commonLookUpservice.CreateCommonLookUp(model);
                    TempData["AlertMessage"] = "CommonLookUp Added Successfully..!";
                    /*return RedirectToAction("Index");*/
                }
                return RedirectToAction("Index");
            }
            /*
                        if (!ModelState.IsValid)
                        {
                            return View(model);
                        }
                        else
                        {
                            commonLookUpservice.CreateCommonLookUp(model);
                            TempData["AlertMessage"] = "CommonLookUp Added Successfully..!";
                            return RedirectToAction("Index");
                        }*/
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
            CommonLookUp commonLookUpToEdit = commonLookUpservice.GetCommonLookUp(Id);

            if (commonLookUpToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    commonLookUpToEdit.ConfigName = commonLookUp.ConfigName;
                    commonLookUpToEdit.ConfigKey = commonLookUp.ConfigKey;
                    commonLookUpToEdit.DisplayOrder = commonLookUp.DisplayOrder;
                    commonLookUpToEdit.Description = commonLookUp.Description;
                    commonLookUpToEdit.ConfigValue = commonLookUp.ConfigValue;
                    commonLookUpToEdit.IsActive = commonLookUp.IsActive;
                    commonLookUpservice.UpdateCommonLookUp(commonLookUpToEdit);
                    TempData["AlertMessage"] = "Common LookUp Updated Successfully..!";
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(Guid Id)
        {

            CommonLookUp commonLookUpToDelete = commonLookUpservice.GetCommonLookUp(Id);
            commonLookUpservice.RemoveCommonLookUp(commonLookUpToDelete);
            TempData["AlertMessage"] = "CommonLookUp Deleted Successfully..!";
            return RedirectToAction("Index");           
        }
    }
}