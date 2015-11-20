using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ChatterSpot.Models;

namespace ChatterSpot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string alertTitle, string alertMessage, string alertType)
        {
            ViewBag.AlertTitle = alertTitle;
            ViewBag.AlertMessage = alertMessage;
            ViewBag.AlertType = alertType;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult PrintableTerms()
        {
            return View();
        }

        public ActionResult Room(string id)
        {
            Guid identifier;

            try
            {
                identifier = new Guid(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { alertTitle = "Error 406", alertMessage = "Room Identifier not acceptable.", alertType = "danger" });
            }
            
            var room = db.Rooms.FirstOrDefault(x => x.RoomIdentifier == identifier);
            if (room == null)
            {
                return RedirectToAction("Index", new {alertTitle = "Error 404", alertMessage = "Room Not Found.", alertType="danger"});
            }

            room.LastAccess = DateTime.Now;
            db.SaveChanges();

            return View(room);
        }

        [HttpGet]
        public JsonResult GetChatLog(int roomId)
        {
            return Json(db.Messages.Where(x => x.RoomId == roomId).OrderBy(x => x.SendDate).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateRoom(string title, string accessPassword, string adminPassword)
        {
            var nRoom = new Room
            {
                Title = title,
                AdministrationPassword = adminPassword,
                LastAccess = DateTime.Now,
                Password = accessPassword,
                RoomIdentifier = Guid.NewGuid()
            };

            db.Rooms.Add(nRoom);
            db.SaveChanges();

            return RedirectToAction("Room", new {id = nRoom.RoomIdentifier});
        }

        [HttpPost]
        public JsonResult SendMessage(string displayName, string body, int roomId)
        {
            var nMessage = new Message
            {
                Body = body,
                DisplayName = displayName,
                RoomId = roomId,
                SendDate = DateTime.Now
            };

            db.Messages.Add(nMessage);
            db.SaveChanges();

            return Json(new {id = 1});
        }

        [HttpPost]
        public JsonResult ValidatePassword(int roomId, string password)
        {
            var passwordMatches = false;

            var room = db.Rooms.Find(roomId);

            passwordMatches = room.Password == password;

            return Json(new {success = passwordMatches});
        }

        [HttpPost]
        public JsonResult ValidateAdminPassword(string password, int roomId)
        {
            var passwordMatches = false;

            var room = db.Rooms.Find(roomId);

            passwordMatches = room.AdministrationPassword == password;

            return Json(new { success = passwordMatches });
        }

        [HttpPost]
        public JsonResult UpdateTitle(string title, int roomId, string password)
        {
            var room = db.Rooms.Find(roomId);

            if (room.AdministrationPassword != password)
            {
                return Json(new {success = false});
            }

            room.Title = title;
            db.SaveChanges();

            return Json(new {success = true});
        }

        [HttpPost]
        public JsonResult UpdatePassword(string newPass, string adminPass, int roomId)
        {
            var room = db.Rooms.Find(roomId);

            if (room.AdministrationPassword != adminPass)
            {
                return Json(new { success = false });
            }

            room.Password = newPass;
            db.SaveChanges();

            return Json(new { success = true });
        }
    }
}