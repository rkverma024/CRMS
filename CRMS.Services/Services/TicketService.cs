using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class TicketService : ITicketService
    {
        ITicketRepository ticketRepository;
        IUserService userService;
        ICommonLookUpService commonLookUpService;
        ITicketAttachmentService ticketAttachmentService;
        public TicketService(ITicketRepository ticketrepository, IUserService UserService, ICommonLookUpService CommonLookUpService, ITicketAttachmentService TicketAttachmentService)
        {
            ticketRepository = ticketrepository;
            userService = UserService;
            commonLookUpService = CommonLookUpService;
            ticketAttachmentService = TicketAttachmentService;
        }

        public void CreateTicket(TicketViewModel viewmodel)
        {
            Ticket model = new Ticket();
            model.Id = viewmodel.Id;
            model.Title = viewmodel.Title;
            model.AssignTo = viewmodel.AssignTo;
            model.TypeId = viewmodel.TypeId;
            model.PriorityId = viewmodel.PriorityId;
            model.StatusId = viewmodel.StatusId;
            model.Description = viewmodel.Description;
            model.CreatedBy = viewmodel.CreatedBy;
            if (viewmodel.Image != null)
            {
                ticketAttachmentService.CreateTicketAttachment(viewmodel);
            }            
            ticketRepository.Insert(model);
            ticketRepository.Commit();
        }
       
        public List<Ticket> GetTicketList()
        {
            return ticketRepository.Collection().Where(b => b.IsDeleted == false).OrderBy(x => x.Title).ToList();
        }

        public Ticket GetTicketById(Guid Id)
        {
            Ticket model = ticketRepository.Find(Id);
            return model;
        }

        public TicketViewModel BindTicketVM(Ticket model)
        {
            TicketViewModel viewmodel = new TicketViewModel();
            viewmodel.Id = model.Id;
            viewmodel.Title = model.Title;
            viewmodel.AssignTo = model.AssignTo;
            viewmodel.TypeId = model.TypeId;
            viewmodel.PriorityId = model.PriorityId;
            viewmodel.StatusId = model.StatusId;
            viewmodel.Description = model.Description;
            viewmodel.TicketImage = ticketAttachmentService.GetTicketIdList(model.Id);
            viewmodel.DropdownAssignTo = userService.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
            viewmodel.DropdownPriorityId = commonLookUpService.GetDropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            viewmodel.DropdownTypeId = commonLookUpService.GetDropDownList("Type").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            viewmodel.DropdownStatusId = commonLookUpService.GetDropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });           
            return viewmodel;
        }
        public void UpdateTicket(TicketViewModel viewmodel)
        {
            Ticket ticketToEdit = GetTicketById(viewmodel.Id);
            
            ticketToEdit.Title = viewmodel.Title;
            ticketToEdit.AssignTo = viewmodel.AssignTo;
            ticketToEdit.TypeId = viewmodel.TypeId;
            ticketToEdit.PriorityId = viewmodel.PriorityId;
            ticketToEdit.StatusId = viewmodel.StatusId;
            ticketToEdit.Description = viewmodel.Description;
            ticketToEdit.UpdatedBy = viewmodel.UpdatedBy;
            ticketToEdit.UpdatedOn = DateTime.Now;
            if (viewmodel.Image != null)
            {
                ticketAttachmentService.CreateTicketAttachment(viewmodel);
            }
            viewmodel.DropdownAssignTo = userService.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
            viewmodel.DropdownPriorityId = commonLookUpService.GetDropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            viewmodel.DropdownTypeId = commonLookUpService.GetDropDownList("Type").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            viewmodel.DropdownStatusId = commonLookUpService.GetDropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            ticketRepository.Update(ticketToEdit);
            ticketRepository.Commit();
        }

        public void RemoveTicket(Ticket model)
        {
            model.IsDeleted = true;
            ticketRepository.Commit();
        }

        public TicketIndexViewModel GetAllTicketLists()
        {
            var status = new TicketIndexViewModel();
            status.Tickets = ticketRepository.AllTicketList().ToList();
            status.StatusDropDown = ticketRepository.StatusFilterList().ToList();
            return status;
        }


        public TicketCommentViewModel TicketDetailsByTicketId(Guid Id)
        {
            return ticketRepository.GetTicketDetailsByTicketId(Id).FirstOrDefault();
        }
    }
}
