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
        List<TicketComment> GetTicketCommentList(Guid TicketId);
        TicketComment GetTicketCommentById(Guid Id);
        TicketCommentViewModel BindTicketComment(TicketComment model);
        void UpdateTicketComment(TicketCommentViewModel viewModel);
        void RemoveTicketComment(TicketComment model);

        IEnumerable<CommentIndexViewModel> GetAllCommentLists(Guid TicketId);
    }
}