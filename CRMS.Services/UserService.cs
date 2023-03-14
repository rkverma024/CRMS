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
    public class UserService : IUserService
    {
        IUserRepository userrepository;
        IUserRoleRepository userRolerepository;
        IRoleRepository rolerepository;

        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            this.userrepository = userRepository;
            this.userRolerepository = userRoleRepository;
            this.rolerepository = roleRepository;
        }

        public void CreateUser(UserViewModel model)
        {
            User user = new User();

            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;
            
            userrepository.Insert(user);
            userrepository.Commit();

            UserRole userrole = new UserRole();
            userrole.UserId = model.UserId;
            userrole.RoleId = model.RoleId;

            userRolerepository.Insert(userrole);
            userRolerepository.Commit();
        }

        public User GetUser(Guid Id)
        {
            User user = userrepository.Find(Id); ;
            return user;
        }

        public List<User> GetUserList()
        {
            return userrepository.Collection().Where(b => b.IsDeleted == false).ToList();
        }

        public void RemoveUser(User removeUser)
        {
            removeUser.IsDeleted = true;
            userrepository.Commit();
        }

        public void UpdateUser(User updateUser)
        {
            userrepository.Update(updateUser);
            userrepository.Commit();
        }
    }
}
