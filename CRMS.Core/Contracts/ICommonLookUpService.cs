using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface ICommonLookUpService
    {
        void CreateCommonLookUp(CommonLookUpViewModel model);
        List<CommonLookUp> GetCommonLookUpsList();
        CommonLookUp GetCommonLookUp(Guid Id);
        void UpdateCommonLookUp(CommonLookUp updatecommonLookUp);
        void RemoveCommonLookUp(CommonLookUp removecommonLookUp);

         bool IsExist(CommonLookUpViewModel model, bool IsAvailable);
    }
}
