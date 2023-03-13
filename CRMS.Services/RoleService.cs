using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class RoleService : IRoleServiceRepository
    {
        RoleRepository rolecontext;

        public RoleService(RoleRepository rolecontext)
        {
            this.rolecontext = rolecontext;
        }

        public void CreateRole(Role role)
        {
            rolecontext.Insert(role);
        }

        public List<Role> GetRolesList()
        {
            return rolecontext.Collection().ToList();
        }

        public Role GetUser(Guid Id)
        {
            return rolecontext.Find(Id);
        }

        public void RemoveUser(Role removeRole)
        {
            removeRole.IsDeleted = true;
            rolecontext.Commit();
        }

        public void UpdateUser(Role updateRole)
        {
            rolecontext.Update(updateRole);
            rolecontext.Commit();
        }
    }
}
