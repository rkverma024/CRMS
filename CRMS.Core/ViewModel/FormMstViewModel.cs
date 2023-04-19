using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class FormMstViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Navigate URL")]
        public string NavigateURL { get; set; }       

        public Guid? ParentFormId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [RegularExpression(@"[A-Z]{1,10}$",
         ErrorMessage = "Only uppercase Characters are allowed.")]
        [Display(Name = "Form Access Code")]
        public string FormAccessCode { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Display Index")]
        public int? DisplayIndex { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }       
        public IEnumerable<DropDown> Dropdown { get; set; }

        [Display(Name = "Parent Form")]
        public string ParentForm { get; set; }
        public string AllowAll { get; set; }
    }
}
