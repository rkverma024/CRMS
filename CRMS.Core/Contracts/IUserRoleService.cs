using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IUserRoleService
    {
        void CreateUserRole(UserRole model);
        List<UserRole> GetUserRoleList();
        UserRole GetUserRole(Guid Id);
        void UpdateUserRole(UserRole updateUserRole);
        void RemoveUserRole(UserRole removeUserRole);
    }
}
