using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.RepositoryInterface
{
    public interface ITicketCommentRepository
    {
        IEnumerable<TicketComment> Collection();
        void Commit();
        void Delete(Guid Id);
        TicketComment Find(Guid Id);
        void Insert(TicketComment model);
        void Update(TicketComment model);
        IEnumerable<CommentIndexViewModel> GetAllCommentList(Guid TicketId);
    }
}
