using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class UserRoleService : IUserRoleService
    {
        IUserRoleRepository userRolerepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            this.userRolerepository = userRoleRepository;
        }
        public void CreateUserRole(UserRole model)
        {
            userRolerepository.Insert(model);
            userRolerepository.Commit();
        }

        public UserRole GetUserRole(Guid Id)
        {
            UserRole userRole = userRolerepository.Collection().Where(x=> x.UserId == Id).FirstOrDefault();
            return userRole;

        }

        public List<UserRole> GetUserRoleList()
        {
            return userRolerepository.Collection().Where(b => b.IsDeleted == false).ToList();
        }

        public void RemoveUserRole(UserRole removeUserRole)
        {
            removeUserRole.IsDeleted = true;
            userRolerepository.Commit();
        }

        public void UpdateUserRole(UserRole updateUserRole)
        {
            userRolerepository.Update(updateUserRole);
            userRolerepository.Commit();
        }
    }
}
