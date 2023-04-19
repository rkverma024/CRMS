using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS.DataAccess.SQL
{
    public class FormMstRepository : IFormMstRepository
    {
        internal DataContext context;
        internal DbSet<FormMst> dbSet;

        public FormMstRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<FormMst>();
        }
        public IQueryable<FormMst> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var formmst = Find(Id);
            if (context.Entry(formmst).State == EntityState.Detached)
            {
                dbSet.Attach(formmst);
            }
            dbSet.Remove(formmst);
        }

        public FormMst Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(FormMst formMst)
        {
            dbSet.Add(formMst);
        }

        public void Update(FormMst formMst)
        {
            dbSet.Attach(formMst);
            context.Entry(formMst).State = EntityState.Modified;
        }
        public List<FormMstViewModel> GetFormMstsIndex()
        {
            var formList = (from form in context.FormMsts.ToList()
                            join fm in context.FormMsts.ToList()
                            on form.ParentFormId equals fm.Id into parentforms
                            from p in parentforms.DefaultIfEmpty()
                            select new FormMstViewModel()
                            {
                                Id = form.Id,
                                Name = form.Name,
                                NavigateURL = form.NavigateURL,
                                ParentForm = p?.Name,
                                ParentFormId = form.ParentFormId,
                                FormAccessCode = form.FormAccessCode,
                                IsActive = form.IsActive,
                                DisplayIndex = form.DisplayIndex,
                            }).ToList();
            return formList;
        }
        public List<FormMstViewModel> NavBarList()
        {
            var userId = (Guid)HttpContext.Current.Session["Id"];
            var loginRoleId = context.UserRoles.Where(x => x.UserId == userId && !x.IsDeleted ).Select(x => x.RoleId).FirstOrDefault();
            //var formrolelist = HttpContext.Current.Session["Permission"] as List<FormRoleMapping>;
            var formList = (from form in context.FormMsts.ToList()
                            join fm in context.FormMsts.ToList()
                            on form.ParentFormId equals fm.Id into parentforms
                            from p in parentforms.DefaultIfEmpty()
                            join formRole in context.FormRoleMappings.ToList()
                            on form.Id equals formRole.FormId
                            where formRole.AllowView == true && formRole.RoleId == loginRoleId
                            select new FormMstViewModel()
                            {
                                Id = form.Id,
                                Name = form.Name,
                                NavigateURL = form.NavigateURL,
                                ParentForm = p?.Name,
                                ParentFormId = form.ParentFormId,
                                FormAccessCode = form.FormAccessCode,
                                IsActive = form.IsActive,
                                DisplayIndex = form.DisplayIndex,
                            }).OrderBy(x => x.DisplayIndex).ToList();
            return formList;
        }
    }
}
