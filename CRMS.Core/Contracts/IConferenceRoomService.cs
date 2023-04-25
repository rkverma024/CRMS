using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core
{
    public interface IConferenceRoomService
    {
        void CreateConferenceRoom(ConferenceRoomViewModel model);
        List<ConferenceRoom> GetConferenceRoomList();
        ConferenceRoom GetConferenceRoomById(Guid Id);
        ConferenceRoomViewModel BindConferenceRoomVW(ConferenceRoom model);
        void UpdateConferenceRoom(ConferenceRoomViewModel model, Guid Id);
        void RemoveConferenceRoom(ConferenceRoom removeConferenceRoom, Guid Id);        
        bool IsExist(ConferenceRoomViewModel model, bool IsAvailable);
    }
}
