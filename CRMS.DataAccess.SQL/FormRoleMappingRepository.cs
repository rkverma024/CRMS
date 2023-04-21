using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class FormRoleMappingRepository : IFormRoleMappingRepository
    {
        internal DataContext context;
        internal DbSet<FormRoleMapping> dbSet;
        public FormRoleMappingRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<FormRoleMapping>();
        }

        public void BulkDelete(IEnumerable<FormRoleMapping> formRoleMapping)
        {
            dbSet.RemoveRange(formRoleMapping);
            Commit();
        }

        public void BulkInsert(IEnumerable<FormRoleMapping> formRoleMapping)
        {
            dbSet.AddRange(formRoleMapping);
            Commit();
        }

        public IQueryable<FormRoleMapping> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var formRolemst = Find(Id);
            if (context.Entry(formRolemst).State == EntityState.Detached)
            {
                dbSet.Attach(formRolemst);
            }
            dbSet.Remove(formRolemst);
        }

        public FormRoleMapping Find(Guid Id)
        {
            return dbSet.Find(Id);
        }
        
        public void Insert(FormRoleMapping formRoleMapping)
        {
            dbSet.Add(formRoleMapping);
        }

        public void Update(FormRoleMapping formRoleMapping)
        {
            dbSet.Attach(formRoleMapping);
            context.Entry(formRoleMapping).State = EntityState.Modified;
        }
        public IEnumerable<FormRoleMappingViewModel> GetFormRights(Guid? Id)
        {
            var viewform = (from fm in context.FormMsts.ToList()
                            join frm in context.FormRoleMappings.ToList()
                            on new { Id = fm?.Id, RoleId = Id } equals new { Id = frm.FormId, RoleId = frm.RoleId } into fs
                            from f in fs.DefaultIfEmpty()
                            select new FormRoleMappingViewModel()
                            {
                                RoleId = Id,
                                FormId = fm.Id,
                                Name = fm.Name,
                                SelectAll = f == null ? false : (f.AllowView && f.AllowInsert && f.AllowEdit && f.AllowDelete) ? true : false,
                                AllowView = f?.AllowView == null ? false : f.AllowView,
                                AllowInsert = f?.AllowInsert == null ? false : f.AllowInsert,
                                AllowEdit = f?.AllowEdit == null ? false : f.AllowEdit,
                                AllowDelete = f?.AllowDelete == null ? false : f.AllowDelete
                            }).OrderBy(x => x.Name).ToList();
            return viewform;
        }
    }
}
