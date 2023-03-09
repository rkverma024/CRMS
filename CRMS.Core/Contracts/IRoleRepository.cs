using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IRoleRepository
    {
        void CreateRole(Role role);
        List<Role> GetRolesList();
        Role GetRole(Guid Id);
        void UpdateRole(Role updateRole);
        
    }
}
