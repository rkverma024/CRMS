using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class ConferenceRoomBookingViewModel :BaseEntity
    {
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Duration")]
        public Guid DurationId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Members")]
        public int Members { get; set; }
    }
}
