using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface ICommanLookUpService
    {
        void CreateCommanLookUp(CommanLookUpViewModel model);
        List<CommanLookUp> GetCommanLookUpsList();
        CommanLookUp GetCommanLookUp(Guid Id);
        void UpdateCommanLookUp(CommanLookUp updatecommanLookUp);
        void RemoveCommanLookUp(CommanLookUp removecommanLookUp);
    }
}
