using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS.Services
{
    public class FormMstService : IFormMstService
    {
        IFormMstRepository formMstrepository;
        IRoleRepository roleRepository;

        public FormMstService(IFormMstRepository formMstRepository, IRoleRepository RoleRepository)
        {
            formMstrepository = formMstRepository;
            roleRepository = RoleRepository;
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
            formMst.IsMenu = model.IsMenu;
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

        public FormMstViewModel BindFormVM(FormMst model)
        {
            FormMstViewModel viewModel = new FormMstViewModel();
            viewModel.Name = model.Name;
            viewModel.NavigateURL = model.NavigateURL;
            viewModel.ParentFormId = model.ParentFormId;
            viewModel.FormAccessCode = model.FormAccessCode;
            viewModel.DisplayIndex = model.DisplayIndex;
            viewModel.IsActive = model.IsActive;
            viewModel.IsMenu = model.IsMenu;
            viewModel.Dropdown = GetFormDropdownList()
                .Where(x => x.ParentFormId == null && x.Id != model.Id)
                .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            return viewModel;
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
            formMstToEdit.IsMenu = model.IsMenu;
            formMstToEdit.UpdatedBy = model.UpdatedBy;
            formMstToEdit.UpdatedOn = DateTime.Now;
            formMstrepository.Update(formMstToEdit);
            formMstrepository.Commit();
        }

        public bool IsExist(FormMstViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetFormMstsList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                             (x.Name.ToLower() == model.Name.ToLower() ||
                                                              x.FormAccessCode.ToLower() == model.FormAccessCode.ToLower())).Any();
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
            return formMstrepository.GetFormMstsIndex();
        }

        public List<FormMstViewModel> NavBarFormList()
        {
            return formMstrepository.NavBarList();
        }
        public List<FormMstViewModel> TabFormLists()
        {
            return formMstrepository.TabFormList();
        }
    }
}
