using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL.Repository
{
    public class TicketCommentRepository : ITicketCommentRepository
    {
        internal DataContext context;
        internal DbSet<TicketComment> dbSet;

        public TicketCommentRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TicketComment>();
        }
        public IEnumerable<TicketComment> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var ticketComment = Find(Id);
            if (context.Entry(ticketComment).State == EntityState.Detached)
            {
                dbSet.Attach(ticketComment);
            }
            dbSet.Remove(ticketComment);
        }

        public TicketComment Find(Guid Id)
        {
            return dbSet.Find(Id);
        } 

        public void Insert(TicketComment model)
        {
            dbSet.Add(model);
        }

        public void Update(TicketComment model)
        {
            dbSet.Attach(model);
            context.Entry(model).State = EntityState.Modified;
        }

        public IEnumerable<CommentIndexViewModel> GetAllCommentList(Guid TicketId)
        {
            var list = (from us in context.Users
                       join tcomment in context.TicketComments.Where(x => x.IsDeleted == false && x.TicketId == TicketId) on us.Id equals tcomment.CreatedBy
                       select new CommentIndexViewModel()
                       {
                           Id = tcomment.Id,
                           TicketId = tcomment.TicketId,
                           Comment = tcomment.Comment,
                           UserName = us.Name,
                           CreatedOn = tcomment.CreatedOn
                       }).ToList();
            return list;
        }

       
    }
}
