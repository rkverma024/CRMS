using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Contracts
{
    public interface IConferenceRoomRepository
    {
        IQueryable<ConferenceRoom> Collection();
        void Commit();
        void Delete(Guid Id);
        ConferenceRoom Find(Guid Id);
        void Insert(ConferenceRoom conferenceRoom);
        void Update(ConferenceRoom updateConferenceRoom);

        void AddConferenceRoom(ConferenceRoomViewModel model);
        List<ConferenceRoom> GetList();
        ConferenceRoom GetById(Guid Id);
        void EditConferenceRoom(ConferenceRoomViewModel model, Guid Id);
        void DeleteConferenceRoom(ConferenceRoom removeConferenceRoom, Guid Id);
        bool Exists(ConferenceRoomViewModel model, bool IsAvailable);
    }
}
