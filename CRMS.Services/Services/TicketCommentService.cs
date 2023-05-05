using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services.Services
{
    public class TicketCommentService : ITicketCommentService
    {
        ITicketCommentRepository ticketCommentRepository;
        public TicketCommentService(ITicketCommentRepository TicketCommentRepository)
        {
            this.ticketCommentRepository = TicketCommentRepository;
        }
        public void CreateTicketComment(TicketCommentViewModel viewmodel)
        {
            TicketComment model = new TicketComment();
            model.Id = viewmodel.Id;
            model.TicketId = viewmodel.TicketId;
            model.Comment = viewmodel.Comment;
        }

        public TicketComment GetTicketCommentById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<TicketCommentViewModel> GetTicketCommentList()
        {
            throw new NotImplementedException();
        }

        public void RemoveTicketComment(TicketComment model)
        {
            throw new NotImplementedException();
        }
        public FormMstViewModel BindTicketComment(TicketComment model)
        {
            throw new NotImplementedException();
        }
        public void UpdateTicketComment(FormMstViewModel TicketComment)
        {
            throw new NotImplementedException();
        }
    }
}
