using CRMS.Core;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        // GET: ConferenceRoomManage
        public ActionResult Index()
        {
            List<ConferenceRoom> conferenceRoom = conferenceroomService.GetConferenceRoomList().ToList();
            return View(conferenceRoom);
        }

        public ActionResult Create()
        {
            ConferenceRoomViewModel conferenceRoom = new ConferenceRoomViewModel();
            return View(conferenceRoom);
        }
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

        public ActionResult Edit(Guid Id)
        {
            ConferenceRoom conferenceRoom = conferenceroomService.GetConferenceRoomById(Id);
            if (conferenceRoom == null)
            {
                return HttpNotFound();
            }
            else
            {
                ConferenceRoomViewModel conferenceRoomroleModel = new ConferenceRoomViewModel();
                conferenceRoomroleModel.ConferenceRoomNo = conferenceRoom.ConferenceRoomNo;
                conferenceRoomroleModel.Capacity = conferenceRoom.Capacity;
                return View(conferenceRoomroleModel);
            }
        }

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
        //[HttpPost]     
        public ActionResult Delete(Guid Id)
        {
            ConferenceRoom conferenceroomToDelete = conferenceroomService.GetConferenceRoomById(Id);
            conferenceroomService.RemoveConferenceRoom(conferenceroomToDelete);
            TempData["DeleteMessage"] = "Added Successfully..!";
            return RedirectToAction("Index");           
        }
    }
}