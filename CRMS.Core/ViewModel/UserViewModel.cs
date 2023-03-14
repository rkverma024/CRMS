using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }                       

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }
        public List<DropDown> RoleDropdown { get; set; }

        /*[Display(Name = "Role")]
        public string Role { get; set; }*/

        public User User { get; set; }
        public UserViewModel()
        {
            UserId = Guid.NewGuid();
        }
    }
}
