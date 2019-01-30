using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeltExam.Models;
using System.Linq;

namespace BeltExam.Controllers
{
    public class DojoActivityController : Controller
    {
        private BeltExamContext dbContext;
        public DojoActivityController( BeltExamContext context)
        {
            dbContext = context;
        }
        /////////////////////////////////////////////////////////////// GET Dashboard //////////
       [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index","LoginReg");
            }
            else
            {
                List<Activity> AllActivity = dbContext.Activity
                    .Include(a =>a.Participants)
                    .ThenInclude(u =>u.User)
                    .OrderByDescending(d=>d.DateTime)
                    .ToList();
                System.Console.WriteLine("*************************** User in session ***************************");
                User User=dbContext.User.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                ViewBag.User = User;
                return View(AllActivity);
            }
        }
         /////////////////////////////////////////////////////////////// GET Add Activity page //////////

        [HttpGet]
        [Route("addActivitypage")]
        public IActionResult AddActivityPage()
        {
            User User=dbContext.User.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.User = User;
            return View();
        }
        /////////////////////////////////////////////////////////////// POST Activity //////////

        [HttpPost]
        [Route("Postactivity")]
        public IActionResult PostActivity(Activity UserSubmission)
        {
            System.Console.WriteLine("*************************** Enter Function ***************************");
            if(ModelState.IsValid)
            {
                dbContext.Add(UserSubmission);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ModelState.AddModelError("Activity", "Error");
            System.Console.WriteLine("*************************** Not Valid ***************************");
            User User=dbContext.User.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.User = User;
            return View("AddActivityPage");
            }
        /////////////////////////////////////////////////////////////// POST Delete Activity //////////
        [HttpGet]
        [Route("deleteactivity/{id}")]
        public IActionResult ActivityWedding(int id)
        {
            System.Console.WriteLine("*************************** Enter Function ***************************");
            var Activity = dbContext.Activity.FirstOrDefault(w => w.ActivityId == id);
            dbContext.Remove(Activity);
            dbContext.SaveChanges();
            User User=dbContext.User.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.User = User;
            return RedirectToAction("Dashboard");
        }
        /////////////////////////////////////////////////////////////// POST Paticipate //////////

        [HttpGet]
        [Route("participant/{id}")]
        public IActionResult Participant(int id)
        {
            Participant NewParticipant = new Participant();
            NewParticipant.UserId = (int)HttpContext.Session.GetInt32("UserId");
            NewParticipant.ActivityId = id;
            dbContext.Add(NewParticipant);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        /////////////////////////////////////////////////////////////// POST Unparticipant //////////

        [HttpGet]
        [Route("unparticipant/{id}")]
        public IActionResult Unparticipant(int id)
        {
            int? UserId = (int)HttpContext.Session.GetInt32("UserId");
            Participant UnParticipant = dbContext.Participant.Where(a =>a.ActivityId == id && a.UserId == UserId).FirstOrDefault();
            dbContext.Remove(UnParticipant);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        /////////////////////////////////////////////////////////////// Get ActivityProfile Page //////////

        [HttpGet]
        [Route("activityprofilepage/{id}")]
        public IActionResult ActivityProfilePage(int id)
        {
            var Activity = dbContext.Activity
    	    .Include(U => U.Participants)
            .ThenInclude(sub => sub.User)
            .FirstOrDefault(A => A.ActivityId == id);
            ViewBag.Creater = dbContext.User.FirstOrDefault(c => c. UserId == Activity.UserId );
            User User=dbContext.User.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.User = User;
            return View(Activity);
        }
        /////////////////////////////////////////////////////////////// GET Logout //////////

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","LoginReg");
        }




    }
}