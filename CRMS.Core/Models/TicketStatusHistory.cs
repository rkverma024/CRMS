﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class TicketStatusHistory : BaseEntity
    {
        public Guid TitleId { get; set; }
        public string NewStatus { get; set; }
        public string OldStatus { get; set; }        
    }
}
