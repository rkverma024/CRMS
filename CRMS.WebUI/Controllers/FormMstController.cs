using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.Services;
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

        [ActionFilter("FMI", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            List<FormMstViewModel> formMsts = formMstservice.GetFormMstsIndexList();          
            return View(formMsts);
        }

        public JsonResult GetFormLists()
        {
            List<FormMstViewModel> formMsts = formMstservice.NavBarFormList();
            return Json(formMsts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormTabList()
        {
            List<FormMstViewModel> formMsts = formMstservice.TabFormLists();
            return Json(formMsts, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter("FMI", CheckRoleRights.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            FormMstViewModel formMstViewModel = new FormMstViewModel();
            formMstViewModel.Dropdown = formMstservice.GetFormDropdownList().Select(b => new DropDown() { Id = b.Id, Name = b.Name }).ToList();
            return View(formMstViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormMstViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Dropdown = formMstservice.GetFormDropdownList().Select(b => new DropDown() { Id = b.Id, Name = b.Name }).ToList();
                return View(model);
            }
            else
            {
                bool existingmodel = formMstservice.IsExist(model, true);
                if (existingmodel)
                {
                    TempData["Already"] = "Already Data is exist.";
                    model.Dropdown = formMstservice.GetFormDropdownList().Select(b => new DropDown() { Id = b.Id, Name = b.Name }).ToList();
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
        }

        [ActionFilter("FMI", CheckRoleRights.FormAccessCode.IsEdit)]
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
                formMstModel.ParentFormId = formMst.ParentFormId;
                formMstModel.FormAccessCode = formMst.FormAccessCode;
                formMstModel.DisplayIndex = formMst.DisplayIndex;
                formMstModel.IsActive = formMst.IsActive;                
                formMstModel.IsMenu = formMst.IsMenu;                
                formMstModel.Dropdown = formMstservice.GetFormDropdownList()
                    .Where(x => x.ParentFormId == null && x.Id != formMst.Id)
                    .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                return View(formMstModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(FormMstViewModel model, Guid Id)
        {
            if (!ModelState.IsValid)
            {
                model.Dropdown = formMstservice.GetFormDropdownList().Select(b => new DropDown() { Id = b.Id, Name = b.Name }).ToList();               
                return View(model);
            }
            else
            {
                bool existingmodel = formMstservice.IsExist(model, false);
                if (existingmodel)
                {
                    TempData["Already"] = "Alredy Data is exist";
                    model.Dropdown = formMstservice.GetFormDropdownList().Select(b => new DropDown() { Id = b.Id, Name = b.Name }).ToList();                   
                    return View(model);
                }
                else
                {
                    model.UpdatedBy = (Guid)Session["Id"];
                    formMstservice.UpdateFormMst(model, Id);
                    TempData["AlertMessage"] = "Updated Successfully..!";
                    return RedirectToAction("Index");
                }
            }
        }

        [ActionFilter("FMI", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            FormMst formMstToDelete = formMstservice.GetFormMstById(Id);
            formMstservice.RemoveFormMst(formMstToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");
        }

        public JsonResult FormMstGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<FormMstViewModel> formMst = formMstservice.GetFormMstsIndexList().ToList();
            return Json(formMst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}