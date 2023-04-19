﻿using CRMS.Core.Contracts;
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
    public class RoleService : IRoleService
    {
        IRoleRepository rolerepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.rolerepository = roleRepository;
        }

        public void CreateRole(RoleViewModel model)
        {
            Role role = new Role();
            role.RoleName = model.RoleName;
            role.Code = model.Code;
            role.CreatedBy = model.CreatedBy;
            //role.CreatedOn = DateTime.Now;
            rolerepository.Insert(role);
            rolerepository.Commit();
        }

        public List<Role> GetRolesList()
        {
            return rolerepository.Collection().Where(b => b.IsDeleted == false).OrderBy(x => x.Code).ToList();
        }

        public Role GetRole(Guid Id)
        {
            Role role = rolerepository.Find(Id); 
            return role;
        }

        public void RemoveRole(Role removeRole)
        {
            
            removeRole.IsDeleted = true;
            rolerepository.Commit();
        }

        public void UpdateRole(RoleViewModel model,Guid Id)
        {
            Role roleToEdit = GetRole(Id);
            roleToEdit.RoleName = model.RoleName;
            roleToEdit.Code = model.Code;
            roleToEdit.UpdatedBy = model.UpdatedBy;
            roleToEdit.UpdatedOn = DateTime.Now;
            rolerepository.Update(roleToEdit);
            rolerepository.Commit();
        }

        public bool IsExist(RoleViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetRolesList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                             (x.RoleName.ToLower() == model.RoleName.ToLower() &&
                                                             (x.Code.ToLower() == model.Code.ToLower()))).Any();
            /*bool existingmodel = GetRolesList().Where(x => x.IsDeleted == false && (IsAvailable || x.Id != model.Id) &&
                                                              x.RoleName.ToLower() == model.RoleName.ToLower()).Any();*/
            if (existingmodel)
            {
                return true;
            }
            return false;
        }
    }
}