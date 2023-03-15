using CRMS.Core.Models;
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
    }
}
