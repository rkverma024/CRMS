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
    public class FormRoleMappingService : IFormRoleMappingService
    {
        IFormRoleMappingRepository formRoleMappingRepository;
        IFormMstService formMstservice;

        public FormRoleMappingService(IFormRoleMappingRepository formroleMappingRepository, IFormMstService formMstService)
        {
            this.formRoleMappingRepository = formroleMappingRepository;
            this.formMstservice = formMstService;
        }

        public IEnumerable<FormRoleMappingViewModel> GetFormRoleRights(Guid Id)
        {
            //IEnumerable<FormRoleMapping> formRoleMapping = formRoleMappingRepository.Collection();
            IEnumerable<FormMstViewModel> formMst = formMstservice.GetFormMstsIndexList();
            List<FormRoleMappingViewModel> formRoleMappingViewModel = new List<FormRoleMappingViewModel>();
            foreach (var item in formMst)
            {
                FormRoleMappingViewModel formRoleViewModel = new FormRoleMappingViewModel();
                formRoleViewModel.FormId = item.Id;
                formRoleViewModel.RoleId = Id;
                formRoleViewModel.Name = item.Name;                
                formRoleMappingViewModel.Add(formRoleViewModel);
            }
            return formRoleMappingViewModel;
        }

        public bool UpdateFormRoleRights(IEnumerable<FormRoleMappingViewModel> formRolerights)
        {
            throw new NotImplementedException();
        }
    }
}
