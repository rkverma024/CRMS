﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class AuditLogsIndexViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ExecutionTime { get; set; }
        //public DateTime ExecutionTime { get; set; }
        public int ExecutionDuration { get; set; }
        public string ClientAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public string Exception { get; set; }
        public int HttpStatusCode { get; set; }
        public string Comments { get; set; }
        public string Parameters { get; set; }
        public string Headres { get; set; }
        public DateTime CreatedOn { get; set; }        
    }

    public class ErrorLogsIndexViewModel
    {
        public Guid Id { get; set; }
        public string Exception { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
