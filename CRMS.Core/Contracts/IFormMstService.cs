using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IFormMstService
    {
        void CreateFormMst(FormMstViewModel model);
        List<FormMst> GetFormMstsList();
        List<FormMstViewModel> GetFormMstsIndexList();
        FormMst GetFormMstById(Guid Id);
        void UpdateFormMst(FormMstViewModel model, Guid ID);
        void RemoveFormMst(FormMst removeformMst);
        bool IsExist(FormMstViewModel model, bool IsAvailable);
        List<FormMst> GetFormDropdownList();
        List<FormMstViewModel> NavBarFormList();
        List<FormMstViewModel> TabFormLists();

    }
}
