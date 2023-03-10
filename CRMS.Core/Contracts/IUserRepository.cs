using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IUserRepository
    {
        //IQueryable Collection();

        //void CreateUser(User user);
        //List<User>GetUsersList();
        //User GetUser(Guid Id);
        //void UpdateUser(User updateUser);
        //void RemoveUser(User removeUser);        

        IQueryable<User> Collection();
        void Commit();
        void Delete(Guid Id);
        User Find(Guid Id);
        void Insert(User user);
        void Update(User updateUser);
    }
}
