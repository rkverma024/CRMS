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
            commonLookUp.CreatedBy = model.CreatedBy;
            /*commonLookUp.CreatedOn = DateTime.Now;*/
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
            return commonLookUprepository.Collection().Where(b => b.IsDeleted == false).OrderBy(x => x.DisplayOrder).ToList();
        }

        public void RemoveCommonLookUp(CommonLookUp removecommonLookUp)
        {
            removecommonLookUp.IsDeleted = true;
            commonLookUprepository.Commit();
        }
        public CommonLookUpViewModel BindCommonLookUpVM(CommonLookUp model)
        {
            CommonLookUpViewModel viewModel = new CommonLookUpViewModel();
            viewModel.ConfigName = model.ConfigName;
            viewModel.ConfigKey = model.ConfigKey;
            viewModel.DisplayOrder = model.DisplayOrder;
            viewModel.Description = model.Description;
            viewModel.ConfigValue = model.ConfigValue;
            viewModel.IsActive = model.IsActive;

            return viewModel;
        }
        public void UpdateCommonLookUp(CommonLookUpViewModel model, Guid Id)
        {
            CommonLookUp commonLookUpToEdit = GetCommonLookUp(model.Id);
            commonLookUpToEdit.ConfigName = model.ConfigName;
            commonLookUpToEdit.ConfigKey = model.ConfigKey;
            commonLookUpToEdit.DisplayOrder = model.DisplayOrder;
            commonLookUpToEdit.Description = model.Description;
            commonLookUpToEdit.ConfigValue = model.ConfigValue;
            commonLookUpToEdit.IsActive = model.IsActive;
            commonLookUpToEdit.UpdatedBy = model.UpdatedBy;
            commonLookUpToEdit.UpdatedOn = DateTime.Now;
            commonLookUprepository.Update(commonLookUpToEdit);
            commonLookUprepository.Commit();
        }

        public bool IsExist(CommonLookUpViewModel model, bool IsAvailable = false)
        {
            bool existingmodel = GetCommonLookUpsList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                             (x.ConfigKey.ToLower() == model.ConfigKey.ToLower() &&
                                                              x.ConfigName.ToLower() == model.ConfigName.ToLower())).Any();
            if (existingmodel)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<CommonLookUp> GetDropDownList(string configname)
        {
            return commonLookUprepository.Collection().Where(b => b.IsDeleted == false && b.ConfigName == configname).ToList();
        }
    }
}