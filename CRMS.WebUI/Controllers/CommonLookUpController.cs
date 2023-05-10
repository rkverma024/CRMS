using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.WebUI.AuditLogFilter;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    
    [Authorize]
    public class CommonLookUpController : Controller
    {
        private ICommonLookUpService commonLookUpservice;

        public CommonLookUpController(ICommonLookUpService CommonLookUpservice)
        {
            commonLookUpservice = CommonLookUpservice;
        }
        [AuditLogsFilter()]
        [ActionFilter("CML", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            List<CommonLookUp> commonLookUp = commonLookUpservice.GetCommonLookUpsList().ToList();
            return PartialView("_CommonListLayout", commonLookUp);
        }
        [AuditLogsFilter()]
        [ActionFilter("CML", CheckRoleRights.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            CommonLookUpViewModel commonlookup = new CommonLookUpViewModel();
            return PartialView("_Create", commonlookup);
        }
        [AuditLogsFilter()]
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
                    model.CreatedBy = (Guid)Session["Id"];
                    commonLookUpservice.CreateCommonLookUp(model);
                    TempData["AlertMessage"] = "Added Successfully..!";
                    //TempData["FormName"] = "CommonLookUp";
                    return Content("true");
                }
            }
        }
        [AuditLogsFilter()]
        [ActionFilter("CML", CheckRoleRights.FormAccessCode.IsEdit)]
        public ActionResult Edit(Guid Id)
        {
            CommonLookUp obj = commonLookUpservice.GetCommonLookUp(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = commonLookUpservice.BindCommonLookUpVM(obj);
                return PartialView("_Edit", viewModel);
            }
        }
        [AuditLogsFilter()]
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
                    commonLookUp.UpdatedBy = (Guid)Session["Id"];
                    commonLookUpservice.UpdateCommonLookUp(commonLookUp, Id);
                    TempData["AlertMessage"] = "Updated Successfully..!";
                    //TempData["FormName"] = "CommonLookUp";
                    return Content("true");
                }
            }
        }
        [AuditLogsFilter()]
        [ActionFilter("CML", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            CommonLookUp commonLookUpToDelete = commonLookUpservice.GetCommonLookUp(Id);
            commonLookUpservice.RemoveCommonLookUp(commonLookUpToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            //TempData["FormName"] = "CommonLookUp";
            return Content("True");
        }
        [AuditLogsFilter()]
        public JsonResult CommonLookUpGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<CommonLookUp> commonLookup = commonLookUpservice.GetCommonLookUpsList().ToList();
            return Json(commonLookup.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}