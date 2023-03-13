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
        IQueryable<User> Collection();
        void Commit();
        void Delete(Guid Id);
        User Find(Guid Id);
        void Insert(User user);
        void Update(User updateUser);
    }
}
