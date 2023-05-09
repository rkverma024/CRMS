using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Collection();
        void Commit();
        void Delete(Guid Id);
        Ticket Find(Guid Id);
        void Insert(Ticket model);
        void Update(Ticket model);
        IEnumerable<TicketListViewModel> AllTicketList();
        IEnumerable<DropDown> StatusFilterList();
        IEnumerable<TicketCommentViewModel> GetTicketDetailsByTicketId(Guid Id);

    }
}
