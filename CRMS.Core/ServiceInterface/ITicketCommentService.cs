using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ServiceInterface
{
    public interface ITicketCommentService
    {
        void CreateTicketComment(TicketCommentViewModel viewmodel);
        List<TicketCommentViewModel> GetTicketCommentList();
        TicketComment GetTicketCommentById(Guid Id);
        FormMstViewModel BindTicketComment(TicketComment model);
        void UpdateTicketComment(FormMstViewModel TicketComment);
        void RemoveTicketComment(TicketComment model);       
    }
}