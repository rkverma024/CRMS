using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IUserService
    {
        void CreateUser(UserViewModel model);
        IEnumerable<IndexViewModel> GetUserRoleList();
        List<User> GetUserList();
        User GetUserById(Guid Id);
        void UpdateUser(UserViewModel model, Guid Id);
        void RemoveUser(User removeUser);

    }
}
