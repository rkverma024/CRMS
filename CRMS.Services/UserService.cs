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
        IUserRoleService _userRoleService;
        IRoleService _roleService;
        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository,
            IUserRoleService userRoleService, IRoleService roleService)
        {
            this.userrepository = userRepository;
            this.userRolerepository = userRoleRepository;
            this.rolerepository = roleRepository;
            this. _userRoleService = userRoleService;
            this._roleService = roleService;
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
            userrole.UserId = user.Id;
            userrole.RoleId = model.RoleId;

            userRolerepository.Insert(userrole);
            userRolerepository.Commit();
        }

        public User GetUserById(Guid Id)
        {
            return userrepository.Find(Id);
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

        public void UpdateUser(UserViewModel model, Guid Id)
        {
            User userToEdit = GetUserById(Id);           

            userToEdit.Name = model.Name;
            userToEdit.Email = model.Email;
            userToEdit.Password = model.Password;

            userrepository.Update(userToEdit);
            userrepository.Commit();

            UserRole userrole = _userRoleService.GetUserRole(userToEdit.Id);
            userrole.RoleId = model.RoleId;           

            userRolerepository.Update(userrole);
            userRolerepository.Commit();
        }

        public IEnumerable<IndexViewModel> GetUserRoleList()
        {

            List<User> user = GetUserList();
            IEnumerable<Role> role = _roleService.GetRolesList();
            IEnumerable<UserRole> userrole = _userRoleService.GetUserRoleList();

            var list = from _user in user
                       join ur in userrole on _user.Id equals ur.UserId
                       join r in role on ur.RoleId equals r.Id
                       select new IndexViewModel()
                       {
                           Id = _user.Id,
                           Name = _user.Name,
                           Email = _user.Email,
                           Role = r.RoleName
                       };
            return list;

        }

       /* public bool IsExist(UserViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetUserList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                              (x.Email == model.Email)).Any();
            if (existingmodel)
            {
                return true;
            }
            return false;
        }  */    
    }
}
