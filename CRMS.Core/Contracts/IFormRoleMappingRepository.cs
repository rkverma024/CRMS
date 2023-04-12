using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public  interface IFormRoleMappingRepository
    {
        IQueryable<FormRoleMapping> Collection();
        void Commit();
        void Delete(Guid Id);
        FormRoleMapping Find(Guid Id);
        void Insert(FormRoleMapping formRoleMapping);
        void Update(FormRoleMapping formRoleMapping);
    }
}
