using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class TicketCommentViewModel : BaseEntity
    {
        public Guid TicketId { get; set; }
        public string Comment { get; set; }       
    }
}
