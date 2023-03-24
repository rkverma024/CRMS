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
        public void UpdateConferenceRoom(ConferenceRoomViewModel model, Guid Id)
        {
            ConferenceRoom conferenceRoomToEdit = GetConferenceRoomById(Id);
            conferenceRoomToEdit.ConferenceRoomNo = model.ConferenceRoomNo;
            conferenceRoomToEdit.Capacity = model.Capacity;
            _conferenceRoomRepository.Update(conferenceRoomToEdit);
            _conferenceRoomRepository.Commit();
        }
        public bool IsExist(ConferenceRoomViewModel model, bool IsAvailable)
        {
            bool existingmodel = GetConferenceRoomList().Where(x => (IsAvailable || x.Id != model.Id) &&
                                                              (x.ConferenceRoomNo.ToLower() == model.ConferenceRoomNo.ToLower())).Any();
            if (existingmodel)
            {
                return true;
            }
            return false;
        }
    }
}
