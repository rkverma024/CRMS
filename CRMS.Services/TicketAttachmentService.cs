using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class TicketAttachmentService : ITicketAttachmentService
    {
        ITicketAttachmentRepository ticketAttachmentRepository;
        public TicketAttachmentService(ITicketAttachmentRepository TicketAttachmentRepository)
        {
            this.ticketAttachmentRepository = TicketAttachmentRepository;
        }
        public void CreateTicketAttachment(TicketViewModel model)
        {
            ticketAttachmentRepository.AddTicketAttachment(model);
        }

        public TicketAttachment GetTicketAttachment(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<TicketAttachment> GetTicketAttachmentList()
        {
            throw new NotImplementedException();
        }

        public void RemoveTicketAttachment(TicketAttachment model)
        {
            model.IsDeleted = true;
            ticketAttachmentRepository.Commit();
        }

        public void BindTicketAttachment(TicketAttachment model)
        {
            throw new NotImplementedException();
        }

        public void EditTicketAttachment(TicketViewModel model)
        {
            //ticketAttachmentRepository.UpdateTicketAttachment(model);
            throw new NotImplementedException();
        }
    }
}
