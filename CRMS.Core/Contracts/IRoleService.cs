using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IRoleService
    {
        void CreateRole(RoleViewModel model);
        List<Role> GetRolesList();
        Role GetRole(Guid Id);
        void UpdateRole(RoleViewModel model, Guid Id);
        void RemoveRole(Role removeRole);
        bool IsExist(RoleViewModel model, bool IsAvailable);
    }
}
