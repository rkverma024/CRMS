using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class TicketStatusHistory
    {
        public Guid Id { get; set; }
        public Guid TitleId { get; set; }
        public string NewStatus { get; set; }
        public string OldStatus { get; set; }       
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public TicketStatusHistory()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}
