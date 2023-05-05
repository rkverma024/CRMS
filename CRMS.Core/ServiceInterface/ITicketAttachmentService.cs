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
        void EditTicketAttachment(TicketViewModel viewmodel);
        List<TicketAttachment> GetTicketAttachmentList();
        TicketAttachment GetTicketAttachmentById(Guid Id);
        TicketAttachment GetTicketId(Guid Id);
        //TicketViewModel BindTicketAttachment(TicketAttachment model);

        void RemoveTicketAttachment(List<string> imagelist);

        IEnumerable<TicketAttachment> GetTicketIdList(Guid ticketId);
    }
}
