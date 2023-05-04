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
       









  

    }
}
