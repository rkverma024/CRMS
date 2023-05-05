using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS.Core.ViewModel
{
    public class TicketViewModel : BaseEntity
    {
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Name")]
        public string Title { get; set; }              

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Assign To")]
        public Guid AssignTo { get; set; }
        public IEnumerable<DropDown> DropdownAssignTo { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Type")]
        public Guid TypeId { get; set; }
        public IEnumerable<DropDown> DropdownTypeId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Priority")]
        public Guid PriorityId { get; set; }
        public IEnumerable<DropDown> DropdownPriorityId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Status")]
        public Guid StatusId { get; set; }
        public IEnumerable<DropDown> DropdownStatusId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public IEnumerable<TicketAttachment> TicketImage{ get; set; }
        public string AttachmentListView { get; set; }
    }

    public class TicketIndexViewModel : BaseEntity
    {       
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

        [Display(Name = "File Name")]
        public int FileName { get; set; }
        public List<DropDown> StatusDropDown { get; set; }
    }
}
