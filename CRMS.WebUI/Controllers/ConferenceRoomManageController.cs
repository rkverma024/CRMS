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
                conferenceroomService.CreateConferenceRoom(model);
                TempData["AlertMessage"] = "Conference Room Added Successfully..!";
                return RedirectToAction("Index");
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
        public ActionResult Edit(ConferenceRoomViewModel conferenceRoom, Guid Id)
        {
            ConferenceRoom conferenceRoomToEdit = conferenceroomService.GetConferenceRoomById(Id);

            if (conferenceRoomToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    conferenceRoomToEdit.ConferenceRoomNo = conferenceRoom.ConferenceRoomNo;
                    conferenceRoomToEdit.Capacity = conferenceRoom.Capacity;
                    conferenceroomService.UpdateConferenceRoom(conferenceRoomToEdit);
                    TempData["AlertMessage"] = "Conference Room Added Successfully..!";
                    return RedirectToAction("Index");
                }
            }
        }
        /*public ActionResult Delete(Guid Id)
        {
            ConferenceRoom conferenceroom = conferenceroomService.GetConferenceRoomById(Id);
            if (conferenceroom == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(conferenceroom);
            }
        }*/

        //[HttpPost]
        //[ActionName("Delete")]
        public ActionResult Delete(Guid Id)
        {
            ConferenceRoom conferenceroomToDelete = conferenceroomService.GetConferenceRoomById(Id);
            conferenceroomService.RemoveConferenceRoom(conferenceroomToDelete);
            TempData["AlertMessage"] = "Conference Room Added Successfully..!";
            return RedirectToAction("Index");
            //if (conferenceroomToDelete == null)
            //{
            //    return HttpNotFound();
            //}
            //else
            //{
            //    conferenceroomService.RemoveConferenceRoom(conferenceroomToDelete);
            //    return RedirectToAction("Index");
            //}
        }
    }
}