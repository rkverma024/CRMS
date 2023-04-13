using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IFormRoleMappingService
    {
        IEnumerable<FormRoleMappingViewModel> GetFormRoleRights(Guid? Id);
        void AddFormRights(IEnumerable<FormRoleMapping> formRoleMapping);
        List<FormRoleMapping> GetList();








        /*  void CreateFormRights(FormRoleMappingViewModel model);
          IEnumerable<FormRoleMapping> GetAllFormRights();        
          void UpdateFormRoleRights(FormRoleMappingViewModel model, Guid Id);*/

        /* List<FormRoleMapping> GetAllForm();*/
        //List<FormRoleViewModel> GetFormRoleIndexList();
        /*  void RemoveFormRole(FormRoleMapping removeformRole);
          bool IsExist(FormRoleMappingViewModel model, bool IsAvailable);*/

    }
}
