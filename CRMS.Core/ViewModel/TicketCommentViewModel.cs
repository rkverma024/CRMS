using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class TicketCommentViewModel : BaseEntity
    {
        public Guid TicketId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Name")]
        public string Title { get; set; }

        [Display(Name = "Assign To")]
        public string AssignTo { get; set; }

        [Display(Name = "Type")]
        public string TypeId { get; set; }

        [Display(Name = "Priority")]
        public string PriorityId { get; set; }

        [Display(Name = "Status")]
        public string StatusId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class CommentIndexViewModel
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
