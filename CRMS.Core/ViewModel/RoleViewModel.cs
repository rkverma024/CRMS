using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role Code")]
        public string RoleCode { get; set; }

        [Required]
        [Display(Name = "Role Type")]
        public string RoleType { get; set; }
        public Role Role { get; set; }
    }
}
