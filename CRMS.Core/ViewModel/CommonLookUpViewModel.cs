using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class CommonLookUpViewModel : BaseEntity
    {        
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Config Name")]
        public string ConfigName { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
      /*  [RegularExpression(@"[A-Z]",ErrorMessage = "Only uppercase Characters are allowed.")]*/
        [Display(Name = "Config Key")]
        public string ConfigKey { get; set; }

        [Display(Name = "Config Value")]
        public string ConfigValue { get; set; }

        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
                
    }
}
