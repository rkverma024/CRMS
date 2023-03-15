using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class ConferenceRoomRepository : IConferenceRoomRepository
    {
        internal DataContext context;
        internal DbSet<ConferenceRoom> dbSet;

        public ConferenceRoomRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<ConferenceRoom>();
        }

        public IQueryable<ConferenceRoom> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var conferenceroom  = Find(Id);
            if (context.Entry(conferenceroom).State == EntityState.Detached)
            {
                dbSet.Attach(conferenceroom);
            }
            dbSet.Remove(conferenceroom);
        }

        public ConferenceRoom Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(ConferenceRoom conferenceRoom)
        {
            dbSet.Add(conferenceRoom);
        }

        public void Update(ConferenceRoom updateConferenceRoom)
        {
            dbSet.Attach(updateConferenceRoom);
            context.Entry(updateConferenceRoom).State = EntityState.Modified;
        }
    }
}
