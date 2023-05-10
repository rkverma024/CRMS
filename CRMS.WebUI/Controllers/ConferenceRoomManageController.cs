using CRMS.Core;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS.Services;
using CRMS.WebUI.AuditLogFilter;

namespace CRMS.WebUI.Controllers
{
    [Authorize]
    public class ConferenceRoomManageController : Controller
    {
        IConferenceRoomService conferenceroomService;

        public ConferenceRoomManageController(IConferenceRoomService conferenceRoomService)
        {
            conferenceroomService = conferenceRoomService;
        }
        [AuditLogsFilter()]
        [ActionFilter("CRM", CheckRoleRights.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            List<ConferenceRoom> conferenceRoom = conferenceroomService.GetConferenceRoomList().ToList();
            return View(conferenceRoom);
        }
        [AuditLogsFilter()]
        [ActionFilter("CRM", CheckRoleRights.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            ConferenceRoomViewModel conferenceRoom = new ConferenceRoomViewModel();
            return View(conferenceRoom);
        }
        [AuditLogsFilter()]
        [HttpPost]
        public ActionResult Create(ConferenceRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                bool existingmodel = conferenceroomService.IsExist(model, true);
                if (existingmodel)
                {
                    TempData["Already"] = "Same Name ConferenceRoom is exist";
                    return View();
                }
                else
                {
                    model.CreatedBy = (Guid)Session["Id"];
                    conferenceroomService.CreateConferenceRoom(model);
                    TempData["AlertMessage"] = "Added Successfully..!";
                    return RedirectToAction("Index");
                }
            }
        }
        [AuditLogsFilter()]
        [ActionFilter("CRM", CheckRoleRights.FormAccessCode.IsEdit)]
        public ActionResult Edit(Guid Id)
        {
            ConferenceRoom obj = conferenceroomService.GetConferenceRoomById(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var conferenceRoomroleModel = conferenceroomService.BindConferenceRoomVW(obj);
                return View(conferenceRoomroleModel);
            }
        }
        [AuditLogsFilter()]
        [HttpPost]
        public ActionResult Edit(ConferenceRoomViewModel model, Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool existingmodel = conferenceroomService.IsExist(model, false);
                if (existingmodel)
                {
                    TempData["Already"] = "Same Name ConferenceRoom is exist";
                    return View();
                }
                else
                {
                    model.UpdatedBy = (Guid)Session["Id"];
                    conferenceroomService.UpdateConferenceRoom(model, Id);
                    TempData["AlertMessage"] = "Added Successfully..!";
                    return RedirectToAction("Index");
                }
            }
        }
        [AuditLogsFilter()]
        [ActionFilter("CRM", CheckRoleRights.FormAccessCode.IsDelete)]
        public ActionResult Delete(Guid Id)
        {
            ConferenceRoom conferenceroomToDelete = conferenceroomService.GetConferenceRoomById(Id);
            conferenceroomService.RemoveConferenceRoom(conferenceroomToDelete, Id);
            TempData["DeleteMessage"] = "Deleted Successfully..!";
            return RedirectToAction("Index");
        }
        [AuditLogsFilter()]
        public JsonResult ConferenceGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ConferenceRoom> conferenceRoom = conferenceroomService.GetConferenceRoomList().ToList();
            return Json(conferenceRoom.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}