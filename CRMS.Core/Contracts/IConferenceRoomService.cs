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
        void UpdateConferenceRoom(ConferenceRoom updateConferenceRoom);
        void RemoveConferenceRoom(ConferenceRoom removeConferenceRoom);
    }
}
