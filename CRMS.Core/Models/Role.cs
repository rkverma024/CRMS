using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class Role : BaseEntity
    {
                 
        public string RoleName { get; set; }
             
        public string Code { get; set; }

        public Role()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

    }
}
