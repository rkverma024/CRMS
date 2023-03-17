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
    public class CommonLookUpService : ICommonLookUpService
    {
        IRepository<CommonLookUp> commonLookUprepository;

        public CommonLookUpService(IRepository<CommonLookUp> commonLookUpRepository)
        {
            this.commonLookUprepository = commonLookUpRepository;
        }

        public void CreateCommonLookUp(CommonLookUpViewModel model)
        {
            CommonLookUp commonLookUp = new CommonLookUp();
            commonLookUp.ConfigName = model.ConfigName;
            commonLookUp.ConfigKey = model.ConfigKey;
            commonLookUp.DisplayOrder = model.DisplayOrder;
            commonLookUp.Description = model.Description;
            commonLookUp.ConfigValue = model.ConfigValue;
            commonLookUprepository.Insert(commonLookUp);
            commonLookUprepository.Commit();

        }

        public CommonLookUp GetCommonLookUp(Guid Id)
        {
            CommonLookUp commonLookUp = commonLookUprepository.Find(Id);
            return commonLookUp;
        }

        public List<CommonLookUp> GetCommonLookUpsList()
        {
            return commonLookUprepository.Collection().Where(b => b.IsDeleted == false).ToList();
        }

        public void RemoveCommonLookUp(CommonLookUp removecommonLookUp)
        {
            removecommonLookUp.IsDeleted = true;
            commonLookUprepository.Commit();
        }

        public void UpdateCommonLookUp(CommonLookUp updatecommonLookUp)
        {
            commonLookUprepository.Update(updatecommonLookUp);
            commonLookUprepository.Commit();
        }
    }
}
