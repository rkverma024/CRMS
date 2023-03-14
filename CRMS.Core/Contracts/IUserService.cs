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
        List<User> GetUserList();
        User GetUser(Guid Id);
        void UpdateUser(User updateUser);
        void RemoveUser(User removeUser);

    }
}
