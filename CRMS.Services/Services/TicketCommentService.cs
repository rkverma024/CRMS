using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS.Services.Services
{
    public class TicketCommentService :Page, ITicketCommentService
    {
        ITicketCommentRepository ticketCommentRepository;
        public TicketCommentService(ITicketCommentRepository TicketCommentRepository)
        {
            ticketCommentRepository = TicketCommentRepository;
        }
        public void CreateTicketComment(TicketCommentViewModel viewmodel)
        {
            TicketComment model = new TicketComment();
            model.Id = viewmodel.Id;
            model.TicketId = viewmodel.TicketId;
            model.Comment = viewmodel.Comment;
            model.CreatedBy = viewmodel.CreatedBy;            
            ticketCommentRepository.Insert(model);
            ticketCommentRepository.Commit();
        }

        public TicketComment GetTicketCommentById(Guid Id)
        {
            TicketComment model = ticketCommentRepository.Find(Id);
            return model;
        }

        public List<TicketComment> GetTicketCommentList(Guid TicketId)
        {
            return ticketCommentRepository.Collection().Where(b => b.IsDeleted == false && b.TicketId == TicketId).ToList();
        }
       
        public TicketCommentViewModel BindTicketComment(TicketComment model)
        {
            TicketCommentViewModel viewModel = new TicketCommentViewModel();
            viewModel.Id = model.Id;
            viewModel.TicketId = model.TicketId;
            viewModel.Comment = model.Comment;           
            return viewModel;                           
        }
        public void UpdateTicketComment(TicketCommentViewModel viewModel)
        {
            TicketComment ticketCommentToEdit =  GetTicketCommentById(viewModel.TicketId);          
            ticketCommentToEdit.Comment = viewModel.Comment;
            ticketCommentToEdit.UpdatedBy = (Guid)Session["Id"];
            ticketCommentToEdit.UpdatedOn = DateTime.Now;
            ticketCommentRepository.Update(ticketCommentToEdit);
            ticketCommentRepository.Commit();
        }
        public void RemoveTicketComment(TicketComment model)
        {
            model.IsDeleted = true;
            ticketCommentRepository.Commit();
        }

        public IEnumerable<CommentIndexViewModel> GetAllCommentLists(Guid TicketId)
        {
            return ticketCommentRepository.GetAllCommentList(TicketId).ToList();
        }
    }
}
