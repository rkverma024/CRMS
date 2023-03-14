using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
   public interface IUserRoleRepository
    {
        IQueryable<UserRole> Collection();
        void Commit();
        void Delete(Guid Id);
        UserRole Find(Guid Id);
        void Insert(UserRole userRole);
        void Update(UserRole updateUserRole);
    }
}
