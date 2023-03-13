using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IRoleServiceRepository
    {
        void CreateRole(Role role);
        List<Role> GetRolesList();
        Role GetUser(Guid Id);
        void UpdateUser(Role updateRole);
        void RemoveUser(Role removeRole);

    }
}
