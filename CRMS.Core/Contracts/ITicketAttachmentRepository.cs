using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface ITicketAttachmentRepository
    {
        IQueryable<TicketAttachment> Collection();
        void Commit();
        void Delete(Guid Id);
        TicketAttachment Find(Guid Id);
        void Insert(TicketAttachment model);
        void Update(TicketAttachment model);

        void AddTicketAttachment(TicketViewModel model);
        //void UpdateTicketAttachment(TicketViewModel model);
    }
}
