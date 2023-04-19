using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IFormMstRepository
    {
        IQueryable<FormMst> Collection();
        void Commit();
        void Delete(Guid Id);
        FormMst Find(Guid Id);
        void Insert(FormMst formMst);
        void Update(FormMst formMst);

        List<FormMstViewModel> GetFormMstsIndex();
        List<FormMstViewModel> NavBarList();
    }
}
