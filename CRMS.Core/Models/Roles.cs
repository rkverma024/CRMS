using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class Roles
    {
        [Key]
        public Guid RoleId { get; set; }

        public string RoleType { get; set; }
    }
}
