using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class CommanLookUpService : ICommanLookUpService
    {
        IRepository<CommanLookUp> commanLookUprepository;

        public CommanLookUpService(IRepository<CommanLookUp> commanLookUpRepository)
        {
            this.commanLookUprepository = commanLookUpRepository;
        }

        public void CreateCommanLookUp(CommanLookUpViewModel model)
        {
            CommanLookUp commanLookUp = new CommanLookUp();
            commanLookUp.ConfigName = model.ConfigName;
            commanLookUp.ConfigKey = model.ConfigKey;
            commanLookUp.DisplayOrder = model.DisplayOrder;
            commanLookUp.Description = model.Description;
            commanLookUp.ConfigValue = model.ConfigValue;
            commanLookUprepository.Insert(commanLookUp);
            commanLookUprepository.Commit();

        }

        public CommanLookUp GetCommanLookUp(Guid Id)
        {
            CommanLookUp commanLookUp = commanLookUprepository.Find(Id);
            return commanLookUp;
        }

        public List<CommanLookUp> GetCommanLookUpsList()
        {
            return commanLookUprepository.Collection().Where(b => b.IsDeleted == false).ToList();
        }

        public void RemoveCommanLookUp(CommanLookUp removecommanLookUp)
        {
            removecommanLookUp.IsDeleted = true;
            commanLookUprepository.Commit();
        }

        public void UpdateCommanLookUp(CommanLookUp updatecommanLookUp)
        {
            commanLookUprepository.Update(updatecommanLookUp);
            commanLookUprepository.Commit();
        }
    }
}
