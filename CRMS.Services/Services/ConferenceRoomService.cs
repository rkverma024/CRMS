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
            _conferenceRoomRepository = conferenceRoomRepository;
        }
        public void CreateConferenceRoom(ConferenceRoomViewModel model)
        {
            _conferenceRoomRepository.AddConferenceRoom(model);
        }

        public ConferenceRoom GetConferenceRoomById(Guid Id)
        {
            return _conferenceRoomRepository.GetById(Id);
        }

        public List<ConferenceRoom> GetConferenceRoomList()
        {
            return _conferenceRoomRepository.GetList();
        }

        public void RemoveConferenceRoom(ConferenceRoom removeConferenceRoom, Guid Id)
        {
            _conferenceRoomRepository.DeleteConferenceRoom(removeConferenceRoom, Id);
        }

        public ConferenceRoomViewModel BindConferenceRoomVW(ConferenceRoom model)
        {
            ConferenceRoomViewModel viewModel = new ConferenceRoomViewModel();
            viewModel.ConferenceRoomNo = model.ConferenceRoomNo;
            viewModel.Capacity = model.Capacity;
            return viewModel;

        }

        public void UpdateConferenceRoom(ConferenceRoomViewModel model, Guid Id)
        {
            _conferenceRoomRepository.EditConferenceRoom(model, Id);
        }
        public bool IsExist(ConferenceRoomViewModel model, bool IsAvailable)
        {
            return _conferenceRoomRepository.Exists(model, IsAvailable);
        }
    }
}
