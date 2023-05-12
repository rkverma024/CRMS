using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS.Services
{
    public class FormRoleMappingService : IFormRoleMappingService
    {
        FormRoleMappingRepository formRoleMappingRepository;
        FormMstRepository fromMstRepository;
        IFormMstService formMstservice;
        IRoleService roleService;

        public FormRoleMappingService(FormRoleMappingRepository formroleMappingRepository, IFormMstService formMstService, IRoleService RoleService, FormMstRepository frommstRepository)
        {
            formRoleMappingRepository = formroleMappingRepository;
            formMstservice = formMstService;
            roleService = RoleService;
            fromMstRepository = frommstRepository;
        }

        public void AddFormRights(IEnumerable<FormRoleMapping> formRoleMapping)
        {
            Guid? roleId = formRoleMapping.FirstOrDefault().RoleId;
            Guid? findRoleId = formRoleMappingRepository.Collection().Where(x => x.RoleId == roleId).Select(x => x.RoleId).FirstOrDefault();
            if (findRoleId == null)
            {
                
                formRoleMappingRepository.BulkInsert(formRoleMapping);
            }
            else
            {
                var delete = formRoleMappingRepository.Collection().Where(x => x.RoleId == findRoleId);
                formRoleMappingRepository.BulkDelete(delete);
                formRoleMappingRepository.BulkInsert(formRoleMapping);
            }
        }
        public IEnumerable<FormRoleMappingViewModel> GetFormRoleRights(Guid? Id)
        {            
            return formRoleMappingRepository.GetFormRights(Id);           
        }

        public List<FormRoleMapping> GetList()
        {
            return formRoleMappingRepository.Collection().ToList();
        }

    }
}
