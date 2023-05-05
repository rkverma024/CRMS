using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.UI;

namespace CRMS.Services
{
    public class TicketAttachmentService : Page, ITicketAttachmentService
    {
        ITicketAttachmentRepository ticketAttachmentRepository;
        
        public TicketAttachmentService(ITicketAttachmentRepository TicketAttachmentRepository)
        {
            this.ticketAttachmentRepository = TicketAttachmentRepository;          
        }
        public void CreateTicketAttachment(TicketViewModel model)
        {            
            TicketAttachment obj = new TicketAttachment();
            obj.TicketId = model.Id;

            string fileExtention = System.IO.Path.GetExtension(model.Image.FileName);
            string imageName = obj.TicketId.ToString() + '_' + DateTime.Now.Ticks + fileExtention;
            string ImagePath = ConfigurationManager.AppSettings["TicketImages"] + imageName;
            model.Image.SaveAs(HostingEnvironment.MapPath(ImagePath));
            obj.FileName = imageName;
            obj.CreatedBy = (Guid)Session["Id"];
            //obj.CreatedBy = model.CreatedBy;
            ticketAttachmentRepository.Insert(obj);
            ticketAttachmentRepository.Commit();
        }

        public void EditTicketAttachment(TicketViewModel viewmodel)
        {
            TicketAttachment obj = GetTicketId(viewmodel.Id);
            if (viewmodel.Image != null)
            {
                CreateTicketAttachment(viewmodel);
            }
            string fileExtention = System.IO.Path.GetExtension(viewmodel.Image.FileName);
            string imageName = obj.TicketId.ToString() + '_' + DateTime.Now.Ticks + fileExtention;
            string ImagePath = ConfigurationManager.AppSettings["TicketImages"] + imageName;
            viewmodel.Image.SaveAs(HostingEnvironment.MapPath(ImagePath));

            obj.FileName = imageName;
            obj.UpdatedBy = (Guid)Session["Id"];
            //obj.UpdatedBy = viewmodel.UpdatedBy;
            obj.UpdatedOn = DateTime.Now;
            ticketAttachmentRepository.Update(obj);
            ticketAttachmentRepository.Commit();
        }
        public TicketAttachment GetTicketAttachmentById(Guid Id)
        {
            TicketAttachment model = ticketAttachmentRepository.Find(Id);
            return model;
        }

        public TicketAttachment GetTicketId(Guid Id)
        {
            return ticketAttachmentRepository.Collection().Where(x => x.Id == x.TicketId && x.IsDeleted == false).FirstOrDefault();           
        }

        public List<TicketAttachment> GetTicketAttachmentList()
        {
            return ticketAttachmentRepository.Collection().Where(x => x.IsDeleted == false).ToList();
        }

        public void RemoveTicketAttachment(List<string> imagelist)
        {
            List<TicketAttachment> list = new List<TicketAttachment>();
            foreach (var img in imagelist)
            {
               list = ticketAttachmentRepository.Collection().Where(x => x.Id.ToString() == img).ToList();
            }
            foreach(var id in list)
            {
                id.IsDeleted = true;
            }
            ticketAttachmentRepository.Commit();
        }

        public IEnumerable<TicketAttachment> GetTicketIdList(Guid ticketId)
        {
            return ticketAttachmentRepository.Collection().Where(x => x.TicketId == ticketId && x.IsDeleted == false).ToList();
        }      
    }
}
