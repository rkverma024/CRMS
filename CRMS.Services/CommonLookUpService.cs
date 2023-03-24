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
            commonLookUp.IsActive = model.IsActive;
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

        public void UpdateCommonLookUp(CommonLookUpViewModel model, Guid Id)
        {
            CommonLookUp commonLookUpToEdit =GetCommonLookUp(model.Id);
            commonLookUpToEdit.ConfigName = model.ConfigName;
            commonLookUpToEdit.ConfigKey = model.ConfigKey;
            commonLookUpToEdit.DisplayOrder = model.DisplayOrder;
            commonLookUpToEdit.Description = model.Description;
            commonLookUpToEdit.ConfigValue = model.ConfigValue;
            commonLookUpToEdit.IsActive = model.IsActive;
            commonLookUprepository.Update(commonLookUpToEdit);
            commonLookUprepository.Commit();
        }

        public bool IsExist(CommonLookUpViewModel model, bool IsAvailable = false)
        {
            bool existingmodel = GetCommonLookUpsList().Where(x =>(IsAvailable || x.Id != model.Id)&& 
                                                             (x.ConfigKey.ToLower() == model.ConfigKey.ToLower() && 
                                                              x.ConfigName.ToLower() == model.ConfigName.ToLower())).Any();
            if (existingmodel)
            {
                return true;
            }
            return false;
          
          
        }
    }
}
