using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrypt;

namespace CRMS.Services
{
    public class UserService : IUserService
    {
        IUserRepository userrepository;
        IUserRoleRepository userRolerepository;       
        IUserRoleService _userRoleService;
        IRoleService _roleService;
        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IUserRoleService userRoleService, IRoleService roleService)
        {
            userrepository = userRepository;
            userRolerepository = userRoleRepository;            
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public void CreateUser(UserViewModel model)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            User user = new User();            
            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;
            user.Password = encoder.Encode(model.Password);
            user.UserName = model.UserName;
            user.Gender = model.Gender;
            user.MobileNo = model.MobileNo;
            user.CreatedBy = model.CreatedBy;            
            userrepository.Insert(user);
            userrepository.Commit();

            UserRole userrole = new UserRole();
            userrole.UserId = user.Id;
            userrole.RoleId = model.Role;

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

        public UserViewModel BindUserVW(User model)
        {
            UserViewModel viewmodel = new UserViewModel();
            viewmodel.Name = model.Name;
            viewmodel.Email = model.Email;
            viewmodel.Password = model.Password;
            viewmodel.UserName = model.UserName;
            viewmodel.Gender = model.Gender;
            viewmodel.MobileNo = model.MobileNo;
            viewmodel.Role = _userRoleService.GetUserRole(model.Id).RoleId;
            viewmodel.RoleDropdown = _roleService.GetRolesList().Select(x => new DropDown() { Id = x.Id, Name = x.RoleName }).ToList();
            return viewmodel;
        }
        public void UpdateUser(UserViewModel model, Guid Id)
        {
            User userToEdit = GetUserById(Id);           

            userToEdit.Name = model.Name;
            userToEdit.Email = model.Email;
            userToEdit.Password = model.Password;
            userToEdit.UserName = model.UserName;
            userToEdit.Gender = model.Gender;
            userToEdit.MobileNo = model.MobileNo;
            userToEdit.UpdatedBy = model.UpdatedBy;
            userToEdit.UpdatedOn = DateTime.Now;
            userrepository.Update(userToEdit);
            userrepository.Commit();

            UserRole userrole = _userRoleService.GetUserRole(userToEdit.Id);
            userrole.RoleId = model.Role;           

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
                           UserName =_user.UserName,
                           Gender = _user.Gender,
                           MobileNo = _user.MobileNo,
                           Role = r.RoleName
                       };
            return list;
        }

        public bool IsExist(UserViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetUserList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                              (x.Email.ToLower() == model.Email.ToLower())).Any();
            if (existingmodel)
            {
                return true;
            }
            return false;
        }       
    }
}
