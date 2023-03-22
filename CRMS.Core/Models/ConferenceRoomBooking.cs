using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class ConferenceRoomBooking : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public Guid DurationId { get; set; }
        public int Members { get; set; }
        public ConferenceRoomBooking()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

    }
}
