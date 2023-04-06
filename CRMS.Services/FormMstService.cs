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
            //formMst.ParentFormId = model.ParentFormId;
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
            //formMstToEdit.ParentFormId = model.ParentFormId;
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
            throw new NotImplementedException();
        }
    }
}
