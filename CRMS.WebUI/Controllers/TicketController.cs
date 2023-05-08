using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CRMS.WebUI.Controllers
{
    public class TicketController : Controller
    {
        ITicketService ticketService;
        IUserService userService;
        ICommonLookUpService commonLookUpService;
        ITicketAttachmentService ticketAttachmentService;
        ITicketCommentService ticketCommentService;
        public TicketController(ITicketService TicketService, IUserService UserService, ICommonLookUpService CommonLookUpService, ITicketAttachmentService TicketAttachmentService, ITicketCommentService TicketCommentService)
        {
            ticketService = TicketService;
            userService = UserService;
            commonLookUpService = CommonLookUpService;
            ticketAttachmentService = TicketAttachmentService;
            ticketCommentService = TicketCommentService;
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
        public ActionResult Edit(TicketViewModel viewmodel, HttpPostedFileBase file)
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
                if (viewmodel.AttachmentListView != null)
                {
                    List<string> imagelist = JsonConvert.DeserializeObject<List<string>>(viewmodel.AttachmentListView);
                    ticketAttachmentService.RemoveTicketAttachment(imagelist);
                }

                viewmodel.Image = file;
                viewmodel.UpdatedBy = (Guid)Session["Id"];
                ticketService.UpdateTicket(viewmodel);
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

        /*  public ActionResult DeleteTicketAttachment(Guid Id)
          {
              TicketAttachment ticketToDelete = ticketAttachmentService.GetTicketAttachmentById(Id);
              ticketAttachmentService.RemoveTicketAttachment(ticketToDelete);
              TempData["DeleteMessage"] = "Deleted Successfully..!";
              return RedirectToAction("Index");
          }*/

        public JsonResult TicketsGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<TicketIndexViewModel> ticket = ticketService.GetAllTicketLists().ToList();
            return Json(ticket.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TicketAttachment(Guid ticketId)
        {
            var list = ticketAttachmentService.GetTicketIdList(ticketId).ToList();
            return PartialView("_imageview", list);
        }

        public FileResult DownloadImg(Guid Id) // Ticket Id
        {
            var getDocument = ticketAttachmentService.GetTicketAttachmentById(Id);
            string contentType = string.Empty;
            if (getDocument != null)
            {
                contentType = "application/force-download";
                string fullPath = Path.Combine(Server.MapPath("~/Content/TicketImages/") + getDocument.FileName);
                return File(fullPath, contentType, getDocument.FileName);
            }
            return null;
        }

        public ActionResult Details(Guid Id)
        {
            TicketCommentViewModel model = ticketService.TicketDetailsByTicketId(Id);
            return View(model);
        }

        /* Here Methods of Ticket Comments*/

        public ActionResult IndexOfTicketComment(Guid TicketId)
        {
            var list = ticketCommentService.GetAllCommentLists(TicketId).ToList();
            return PartialView("_IndexOfTicketComment", list);
        }

        public ActionResult CreateTicketComment(Guid TicketId)
        {
            TicketCommentViewModel ticketComment = new TicketCommentViewModel();
            ticketComment.TicketId = TicketId;
            return PartialView("_CreateTicketComment", ticketComment);
        }

        [HttpPost]
        public ActionResult CreateTicketComment(TicketCommentViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }
            else
            {
                viewmodel.CreatedBy = (Guid)Session["Id"];
                ticketCommentService.CreateTicketComment(viewmodel);
                TempData["AlertMessage"] = "Added Successfully..!";
                return Content("true");
            }
        }

        public ActionResult EditTicketComment(Guid Id)
        {
            TicketComment obj = ticketCommentService.GetTicketCommentById(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var ticketComment = ticketCommentService.BindTicketComment(obj);
                return View(ticketComment);
            }
        }

        [HttpPost]
        public ActionResult EditTicketComment(TicketCommentViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                ticketCommentService.UpdateTicketComment(viewmodel);
                TempData["AlertMessage"] = "Updated Successfully..!";
                return RedirectToAction("Details");
            }
        }
        public ActionResult DeleteTicketComment(Guid Id)
        {
            TicketComment ticketToDelete = ticketCommentService.GetTicketCommentById(Id);
            ticketCommentService.RemoveTicketComment(ticketToDelete);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return Content("True");
        }
    }
}