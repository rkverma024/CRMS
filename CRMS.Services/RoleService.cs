using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
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
        IRoleRepository rolecontext;

        public RoleService(IRoleRepository rolecontext)
        {
            this.rolecontext = rolecontext;
        }

        public void CreateRole(RoleViewModel model)
        {
            Role role = new Role();
            role.RoleName = model.RoleName;
            role.Code = model.Code;
            rolecontext.Insert(role);
            rolecontext.Commit();
        }

        public List<Role> GetRolesList()
        {
            return rolecontext.Collection().Where(b => b.IsDeleted == false).ToList();
        }

        public Role GetRole(Guid Id)
        {
            Role role = rolecontext.Find(Id); ;
            return role;
        }

        public void RemoveRole(Role removeRole)
        {
            removeRole.IsDeleted = true;
            rolecontext.Commit();
        }

        public void UpdateRole(Role updateRole)
        {
            rolecontext.Update(updateRole);
            rolecontext.Commit();
        }
    }
}
