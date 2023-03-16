using CRMS.Core;
using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class ConferenceRoomService : IConferenceRoomService
    {
        IConferenceRoomRepository _conferenceRoomRepository;

        public ConferenceRoomService(IConferenceRoomRepository conferenceRoomRepository)
        {
            this._conferenceRoomRepository = conferenceRoomRepository;
        }
        public void CreateConferenceRoom(ConferenceRoomViewModel model)
        {
            ConferenceRoom conferenceRoom = new ConferenceRoom();
            conferenceRoom.ConferenceRoomNo = model.ConferenceRoomNo;
            conferenceRoom.Capacity = model.Capacity;
            _conferenceRoomRepository.Insert(conferenceRoom);
            _conferenceRoomRepository.Commit();
        }

       /* public void ConferenceRoom DeleteConferenceRoom(Guid Id)
        {
            ConferenceRoom conferenceRoom = GetConferenceRoomById(Id);
            _conferenceRoomRepository.Delete(Id);


        }
*/
        public ConferenceRoom GetConferenceRoomById(Guid Id)
        {
            ConferenceRoom conferenceRoom = _conferenceRoomRepository.Find(Id);
            return conferenceRoom;
        }

        public List<ConferenceRoom> GetConferenceRoomList()
        {
            return _conferenceRoomRepository.Collection().Where(b => b.IsDeleted == false).ToList();
        }

        public void RemoveConferenceRoom(ConferenceRoom removeConferenceRoom)
        {
            removeConferenceRoom.IsDeleted = true;
            _conferenceRoomRepository.Commit();

        }

        public void UpdateConferenceRoom(ConferenceRoom updateConferenceRoom)
        {
            _conferenceRoomRepository.Update(updateConferenceRoom);
            _conferenceRoomRepository.Commit();
        }
    }
}
