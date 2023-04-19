using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
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


        public void AddConferenceRoom(ConferenceRoomViewModel model)
        {
            ConferenceRoom conferenceRoom = new ConferenceRoom();
            conferenceRoom.ConferenceRoomNo = model.ConferenceRoomNo;
            conferenceRoom.Capacity = model.Capacity;
            conferenceRoom.CreatedBy = model.CreatedBy;            
            Insert(conferenceRoom);
            Commit();
        }
        public ConferenceRoom GetById(Guid Id)
        {
            return Find(Id);
        }       
      
        public List<ConferenceRoom> GetList()
        {
            return Collection().Where(b => b.IsDeleted == false).ToList();
        }
        
        public void DeleteConferenceRoom(ConferenceRoom removeConferenceRoom, Guid Id)
        {
            removeConferenceRoom.IsDeleted = true;
        }

        public void EditConferenceRoom(ConferenceRoomViewModel model, Guid Id)
        {
            ConferenceRoom conferenceRoomToEdit = GetById(Id);
            conferenceRoomToEdit.ConferenceRoomNo = model.ConferenceRoomNo;
            conferenceRoomToEdit.Capacity = model.Capacity;
            conferenceRoomToEdit.UpdatedBy = model.UpdatedBy;
            conferenceRoomToEdit.UpdatedOn = DateTime.Now;
            Update(conferenceRoomToEdit);
            Commit();
        }
        public bool Exists(ConferenceRoomViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                              (x.ConferenceRoomNo.ToLower() == model.ConferenceRoomNo.ToLower())).Any();
            if (existingmodel)
            {
                return true;
            }
            return false;
        }
    }
}
