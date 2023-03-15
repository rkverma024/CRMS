using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class ConferenceRoom : BaseEntity
    {
        public string ConferenceRoomNo { get; set; }
        public int Capacity { get; set; }
        public ConferenceRoom()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}
