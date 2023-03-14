using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class UserRole : BaseEntity
    {    
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }
        public UserRole()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

    }
}
