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
    public class RoleService : IRoleRepository
    {
        SqlRepository<Role> rolecontext;

        public RoleService(SqlRepository<Role> rolecontext)
        {
            this.rolecontext = rolecontext;
        }

        public void CreateRole(Role role)
        {
            rolecontext.Insert(role);
        }

        public Role GetRole(Guid Id)
        {
            return rolecontext.Find(Id);
        }

        public Role GetRole(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRolesList()
        {
            return rolecontext.Collection().ToList();
        }

        public void RemoveRole(Role removerole)
        {
            removerole.IsDeleted = true;
            rolecontext.Commit();
        }
    
        public void UpdateRole(Role updateRole)
        {
            rolecontext.Update(updateRole);
            rolecontext.Commit();
        }
    }
}
