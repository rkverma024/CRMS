using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface ITicketService
    {
        void CreateTicket(TicketViewModel viewmodel);       
        List<Ticket> GetTicketList();
        Ticket GetTicketById(Guid Id);        
        TicketViewModel BindTicketVM(Ticket model);
        void UpdateTicket(TicketViewModel viewmodel);
        void RemoveTicket(Ticket model);
        TicketIndexViewModel GetAllTicketLists();

        TicketCommentViewModel TicketDetailsByTicketId(Guid Id);
        //IEnumerable<TicketIndexViewModel> GetStatus();
        /*bool IsExist(TicketViewModel viewmodel, bool IsAvailable);*/
    }
}
