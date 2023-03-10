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
    public class UserService : IUserServiceRepository
    {
        UserRepository usercontext;
        public UserService(UserRepository usercontext)
        {
            this.usercontext = usercontext;
        }
        public void CreateUser(User user)
        {
            usercontext.Insert(user);
        }

        public List<User> GetRolesList()
        {
            return usercontext.Collection().ToList();
        }

        public User GetUser(Guid Id)
        {
            return usercontext.Find(Id);
        }

        public void RemoveUser(User removeUser)
        {
            removeUser.IsDeleted = true;
            usercontext.Commit();
        }

        public void UpdateUser(User updateUser)
        {
            usercontext.Update(updateUser);
            usercontext.Commit();
        }
    }
}
