using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class FormMstController : Controller
    {
        private IFormMstService formMstservice;

        public FormMstController(IFormMstService formMstService)
        {
            formMstservice = formMstService;
        }

        // GET: FormMst
        public ActionResult Index()
        {
            List<FormMst> formMsts = formMstservice.GetFormMstsList().ToList();
            return View(formMsts);
        }
        public ActionResult Create()
        {
            FormMstViewModel formMstViewModel = new FormMstViewModel();
            return View(formMstViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormMstViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                model.CreatedBy = (Guid)Session["Id"];
                formMstservice.CreateFormMst(model);
                TempData["AlertMessage"] = "Added Successfully..!";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(Guid Id)
        {
            FormMst formMst = formMstservice.GetFormMstById(Id);
            if (formMst == null)
            {
                return HttpNotFound();
            }
            else
            {
                FormMstViewModel formMstModel = new FormMstViewModel();
                formMstModel.Name = formMst.Name;
                formMstModel.NavigateURL = formMst.NavigateURL;
                //formMstModel.ParentFormId = formMst.ParentFormId;
                formMstModel.FormAccessCode = formMst.FormAccessCode;
                formMstModel.DisplayIndex = formMst.DisplayIndex;
                return View(formMstModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormMstViewModel model, Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                model.UpdatedBy = (Guid)Session["Id"];
                formMstservice.UpdateFormMst(model, Id);
                TempData["AlertMessage"] = "Updated Successfully..!";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(Guid Id)
        {           
            FormMst formMstToDelete = formMstservice.GetFormMstById(Id);
            formMstservice.RemoveFormMst(formMstToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");
        }

        public JsonResult FormMstGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<FormMst> formMst = formMstservice.GetFormMstsList().ToList();
            return Json(formMst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}