using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class ConferenceRoomViewModel : BaseEntity
    {
        //public Guid Id { get; set; }
        [Required]
        [Display(Name = "Conference Room No.")]
        public string ConferenceRoomNo { get; set; }
        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
    }
}
