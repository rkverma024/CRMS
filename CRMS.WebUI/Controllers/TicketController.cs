using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class TicketController : Controller
    {
        ITicketService ticketService;
        IUserService userService;
        ICommonLookUpService commonLookUpService;
        public TicketController(ITicketService TicketService, IUserService UserService, ICommonLookUpService CommonLookUpService)
        {
            ticketService = TicketService;
            userService = UserService;
            commonLookUpService = CommonLookUpService;
        }

        [ActionFilter("TT", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            var list = ticketService.GetAllTicketLists().ToList();
            return View(list);
        }

        [ActionFilter("TT", CheckRoleRights.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            TicketViewModel ticket = new TicketViewModel();
            ticket.DropdownAssignTo = userService.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
            ticket.DropdownPriorityId = commonLookUpService.GetDropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            ticket.DropdownTypeId = commonLookUpService.GetDropDownList("Type").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            ticket.DropdownStatusId = commonLookUpService.GetDropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            return View(ticket);
        }

        [HttpPost]
        public ActionResult Create(TicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                model.CreatedBy = (Guid)Session["Id"];
                ticketService.CreateTicket(model);
                TempData["AlertMessage"] = "Added Successfully..!";
                return RedirectToAction("Index", "Ticket");
            }
        }

        [ActionFilter("TT", CheckRoleRights.FormAccessCode.IsEdit)]
        public ActionResult Edit(Guid Id)
        {
            Ticket obj = ticketService.GetTicketById(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var ticket = ticketService.BindTicketVM(obj);
                return View(ticket);
            }
        }
        [HttpPost]
        public ActionResult Edit(TicketViewModel viewmodel, Guid Id)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.DropdownAssignTo = userService.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
                viewmodel.DropdownPriorityId = commonLookUpService.GetDropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                viewmodel.DropdownTypeId = commonLookUpService.GetDropDownList("Type").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                viewmodel.DropdownStatusId = commonLookUpService.GetDropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                return View();
            }
            else
            {
                viewmodel.UpdatedBy = (Guid)Session["Id"];
                ticketService.UpdateTicket(viewmodel, Id);
                TempData["AlertMessage"] = "Updated Successfully..!";
                return RedirectToAction("Index");

            }
        }

        [ActionFilter("TT", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            Ticket ticketToDelete = ticketService.GetTicketById(Id);
            ticketService.RemoveTicket(ticketToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");
        }
        public JsonResult TicketsGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Ticket> ticket = ticketService.GetTicketList().ToList();
            return Json(ticket.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}