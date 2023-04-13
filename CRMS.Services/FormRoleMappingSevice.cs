using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.formRoleMappingRepository = formroleMappingRepository;
            this.formMstservice = formMstService;
            this.roleService = RoleService;
            this.fromMstRepository = frommstRepository;
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
            var viewform = (from fm in fromMstRepository.Collection().ToList()
                            join frm in formRoleMappingRepository.Collection().ToList()
                            on new { Id = fm?.Id, RoleId = Id } equals new { Id = frm.FormId, RoleId = frm.RoleId } into fs
                            from f in fs.DefaultIfEmpty()
                            select new FormRoleMappingViewModel()
                            {
                                RoleId = Id,
                                FormId = fm.Id,
                                Name = fm.Name,
                                SelectAll = f == null ? false : (f.AllowView && f.AllowInsert && f.AllowEdit && f.AllowDelete) ? true : false,
                                //(      (f?.AllowView && f?.AllowInsert ? false : f.AllowView) 
                                //    && (f?.AllowInsert == null ? false : f.AllowInsert) 
                                //    && (f?.AllowEdit == null ? false : f.AllowEdit) 
                                //    && (f?.AllowDelete == null ? false : f.AllowDelete)
                                // ) 
                                //? true : false,
                                AllowView = f?.AllowView == null ? false : f.AllowView,
                                AllowInsert = f?.AllowInsert == null ? false : f.AllowInsert,
                                AllowEdit = f?.AllowEdit == null ? false : f.AllowEdit,
                                AllowDelete = f?.AllowDelete == null ? false : f.AllowDelete
                            }).ToList();
            return viewform;














            /* var viewform = (from f in formMstservice.GetFormMstsList()
                                        join fr in GetList() on f.Id equals fr.FormId
                                        where (fr.RoleId == Id)
                                        select new FormRoleMappingViewModel()
                                        {
                                            Name = f.Name,
                                            FormId = f.Id,
                                            RoleId = fr.RoleId,
                                            AllowInsert = fr.AllowInsert,
                                            AllowDelete = fr.AllowDelete,
                                            AllowEdit = fr.AllowEdit,
                                            AllowView = fr.AllowView,
                                        }).ToList();
             return viewform;*/


            /*IEnumerable<FormRoleMapping> formRoleMapping = formRoleMappingRepository.Collection();
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
            return formRoleMappingViewModel;*/
        }

        public List<FormRoleMapping> GetList()
        {
            return formRoleMappingRepository.Collection().ToList();
        }
    }
}
