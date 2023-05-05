using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
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
    }
}
