using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.Models
{
    public class FormMst
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string NavigateURL { get; set; }
        public Guid? ParentFormId { get; set; }
        public string FormAccessCode { get; set; }
        public int? DisplayIndex { get; set; }       
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public FormMst()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}
