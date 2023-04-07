using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class FormMstService : IFormMstService
    {
        IFormMstRepository formMstrepository;

        public FormMstService(IFormMstRepository formMstRepository)
        {
            this.formMstrepository = formMstRepository;
        }
        public List<FormMst> GetFormMstsList()
        {

            return formMstrepository.Collection().ToList();
        }

        public void CreateFormMst(FormMstViewModel model)
        {
            FormMst formMst = new FormMst();
            formMst.Name = model.Name;
            formMst.NavigateURL = model.NavigateURL;
            formMst.ParentFormId = model.ParentFormId;
            formMst.FormAccessCode = model.FormAccessCode;
            formMst.DisplayIndex = model.DisplayIndex;
            formMst.IsActive = model.IsActive;
            formMst.CreatedBy = model.CreatedBy;
            formMstrepository.Insert(formMst);
            formMstrepository.Commit();
        }

        public FormMst GetFormMstById(Guid Id)
        {
            FormMst formMst = formMstrepository.Find(Id);
            return formMst;
        }

        public void RemoveFormMst(FormMst removeformMst)
        {
            formMstrepository.Delete(removeformMst.Id);
            formMstrepository.Commit();

        }

        public void UpdateFormMst(FormMstViewModel model, Guid ID)
        {
            FormMst formMstToEdit = GetFormMstById(ID);
            formMstToEdit.Name = model.Name;
            formMstToEdit.NavigateURL = model.NavigateURL;
            formMstToEdit.ParentFormId = model.ParentFormId;
            formMstToEdit.FormAccessCode = model.FormAccessCode;
            formMstToEdit.DisplayIndex = model.DisplayIndex;
            formMstToEdit.IsActive = model.IsActive;
            formMstToEdit.UpdatedBy = model.UpdatedBy;
            formMstToEdit.UpdatedOn = DateTime.Now;
            formMstrepository.Update(formMstToEdit);
            formMstrepository.Commit();
        }

        public bool IsExist(FormMstViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetFormMstsList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                             (x.Name.ToLower() == model.Name.ToLower())).Any();
            /*bool existingmodel = GetRolesList().Where(x => x.IsDeleted == false && (IsAvailable || x.Id != model.Id) &&
                                                              x.RoleName.ToLower() == model.RoleName.ToLower()).Any();*/
            if (existingmodel)
            {
                return true;
            }
            return false;
        }

        public List<FormMst> GetFormDropdownList()
        {
            return formMstrepository.Collection().Where(b => b.ParentFormId == null).ToList();
        }

        public List<FormMstViewModel> GetFormMstsIndexList()
        {
            var forms = formMstrepository.Collection().ToList();           
            var formList = (from form in forms
                            join fm in forms
                            on form.ParentFormId equals fm.Id into parentforms
                            from p in parentforms.DefaultIfEmpty()
                            select new FormMstViewModel()
                            {
                                Id = form.Id,
                                Name = form.Name,
                                NavigateURL = form.NavigateURL,
                                ParentForm = p?.Name,
                                ParentFormId = form.ParentFormId,
                                FormAccessCode = form.FormAccessCode,
                                IsActive = form.IsActive,
                                DisplayIndex = form.DisplayIndex
                            }).ToList();

            //foreach (var fms in query)
            //{
            //    FormMstViewModel formMst = new FormMstViewModel();
            //    formMst.Id = fms.Id;
            //    formMst.Name = fms.Name;
            //    formMst.NavigateURL = fms.NavigateURL;
            //    formMst.ParentForm = ;
            //}
            return formList;


        }
    }
}
