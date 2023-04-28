using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface ITicketAttachmentService
    {
        void CreateTicketAttachment(TicketViewModel model);
        void EditTicketAttachment(TicketViewModel model);
        List<TicketAttachment> GetTicketAttachmentList();
        TicketAttachment GetTicketAttachment(Guid Id);
        void BindTicketAttachment(TicketAttachment model);
        
        void RemoveTicketAttachment(TicketAttachment model);
    }
}
