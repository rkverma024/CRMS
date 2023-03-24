using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class UserViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }                       

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public List<DropDown> RoleDropdown { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "MobileNo is Required")]
        //[DataType(DataType.PhoneNumber)]
        //[StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        [Display(Name = "MobileNo")]
        public string MobileNo { get; set; }

        /*[Display(Name = "Role")]
        public string Role { get; set; }*/

        public User User { get; set; }
     
    }
}
