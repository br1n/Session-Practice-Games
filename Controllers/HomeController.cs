using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CounterGame.Models;
using Microsoft.AspNetCore.Http;

namespace CounterGame.Controllers
{
    public class HomeController : Controller
    {
        private int? SessionCount
        {
            get { return HttpContext.Session.GetInt32("count"); }
            set { HttpContext.Session.SetInt32("count", (int)value); }
        }
        private int? SessionGold
        {
            get { return HttpContext.Session.GetInt32("gold"); }
            set { HttpContext.Session.SetInt32("gold", (int)value); }
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            //is count in session?
            if(SessionCount == null)
            {
                SessionCount = 0;
            } 
            ViewBag.Count = SessionCount;
            //is name in session?
            string sessionName = HttpContext.Session.GetString("name");
            if(sessionName == null)
            {
                HttpContext.Session.SetString("name", "New User");
            }

            ViewBag.User = HttpContext.Session.GetString("name");
            return View();
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            //TODO Get session Count
            int? currCount = SessionCount;
            //TODO: increase session count
            currCount += 1;
            //update session with new value
            SessionCount = currCount;
            return RedirectToAction("Index");
        }

        [HttpPost("newUser")]
        public IActionResult NewUser(string username)
        {
            //TODO GET name
            HttpContext.Session.SetString("name", username);
            return RedirectToAction("Index");
        }

        [HttpGet("ninjagold")]
        public IActionResult NinjaGold()
        {
            if(SessionGold == null)
            {
                SessionGold = 0;
            }
            ViewBag.Gold = SessionGold;
            return View();
        }

        [HttpPost("getGold")]
        public IActionResult Gold(Building building)
        {
            Random r = new Random();
            int newGold = r.Next(building.MinGold, building.MaxGold);
            SessionGold += newGold;


            TempData["message"] = ($"You got {newGold} golds from {building.Name}");
            return RedirectToAction("NinjaGold");
        }

    }
}
