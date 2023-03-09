using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface UserRepository
    {
        void CreateUser(User user);
        List<User>GetUsersList();
        User GetUser(Guid Id);
        void UpdateUser(Role updateUser);


    }
}
