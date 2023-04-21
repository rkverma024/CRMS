using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class FormRoleMappingViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Guid? FormId { get; set; }
             
        public Guid? RoleId { get; set; }
       
        [Display(Name = "Insert")]
        public bool AllowInsert { get; set; }
       
        [Display(Name = "Edit")]
        public bool AllowEdit { get; set; }
       
        [Display(Name = "Delete")]
        public bool AllowDelete { get; set; }

        [Display(Name = "View")]
        public bool AllowView { get; set; }

        [Display(Name="All")]
        public bool SelectAll { get; set; }      
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
