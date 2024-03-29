﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class CommonLookUp : BaseEntity
    {        
        public string ConfigName { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }

        public CommonLookUp()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;           
        }
    }
}
