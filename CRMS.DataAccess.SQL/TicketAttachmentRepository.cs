using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;

namespace CRMS.DataAccess.SQL
{
    public class TicketAttachmentRepository : Page, ITicketAttachmentRepository
    {
        internal DataContext context;
        internal DbSet<TicketAttachment> dbSet;
        public TicketAttachmentRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TicketAttachment>();
        }

        public IQueryable<TicketAttachment> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var ticketattachment = Find(Id);
            if (context.Entry(ticketattachment).State == EntityState.Detached)
            {
                dbSet.Attach(ticketattachment);
            }
            dbSet.Remove(ticketattachment);
        }

        public TicketAttachment Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(TicketAttachment model)
        {
            dbSet.Add(model);
        }

        public void Update(TicketAttachment model)
        {
            dbSet.Attach(model);
            context.Entry(model).State = EntityState.Modified;
        }

        public void AddTicketAttachment(TicketViewModel model)
        {
            TicketAttachment obj = new TicketAttachment();
            obj.TicketId = model.Id;

            //string date = DateTime.Now.Ticks.ToString("yyyyMMddHHmmss");
            string fileExtention = System.IO.Path.GetExtension(model.Image.FileName);
            string imageName = obj.TicketId.ToString() + '_' + DateTime.Now.Ticks + fileExtention;
            string ImagePath = ConfigurationManager.AppSettings["TicketImages"] + imageName;
            model.Image.SaveAs(HostingEnvironment.MapPath(ImagePath));
            obj.FileName = imageName;
            obj.CreatedBy = model.CreatedBy;
            Insert(obj);
            Commit();


            /*  TicketAttachment obj = new TicketAttachment();
              obj.TicketId = model.Id;

              string fileExtention = System.IO.Path.GetExtension(model.Image.FileName);
              string imageName = obj.TicketId.ToString() + '_' + DateTime.Now.Ticks + fileExtention;
              string ImagePath = "~/Content/TicketImages/" + imageName;
              model.Image.SaveAs(HostingEnvironment.MapPath(ImagePath));
              obj.FileName = imageName;
              obj.CreatedBy = model.CreatedBy;
              Insert(obj);
              Commit();*/
        }


        /* public void UpdateTicketAttachment(TicketViewModel model)
         {
             string fileName = System.IO.Path.GetFileName(model.Image.FileName);
             string filePath = "/Content/TicketImages/" + fileName;
             model.Image.SaveAs(HostingEnvironment.MapPath(filePath));

             TicketAttachment obj = new TicketAttachment();
             obj.TicketId = model.Id;
             obj.FileName = fileName;
             obj.UpdatedBy = model.UpdatedBy;
             obj.UpdatedOn = DateTime.Now;
             Update(obj);
             Commit();
         }*/
    }
}
