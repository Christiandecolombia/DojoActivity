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
    public class LoginRegController : Controller
    {
        private const string ControllerName = "Activity";
        private BeltExamContext dbContext;
        public LoginRegController( BeltExamContext context)
        {
            dbContext = context;
        }
        /////////////////////////////////////////////////////////////// GET HOME //////////
            [HttpGet]
            [Route("")]
            public IActionResult Index()
            {
                return View();
            }
        /////////////////////////////////// POST REGISTARATION /////////////////////////////

            [HttpPost]
            [Route("registrationpost")]
            public IActionResult RegistrationPost(User UserSubmission)
            {
                System.Console.WriteLine("******************************************** Enter function ********************************************");
                if(ModelState.IsValid)
                {
                    System.Console.WriteLine("******************************************** ModelState is valid ********************************************");
                    if(dbContext.User.Any(u => u.Email == UserSubmission.Email))
                        {
                            System.Console.WriteLine("******************************************** Email is already in the databse.Retrun error ********************************************");
                            ModelState.AddModelError("Email", "Email already in use!");
                            return View("Index");
                        }
                    else
                        {
                            System.Console.WriteLine("******************************************** Email valid, Hashing Password ********************************************");
                            PasswordHasher<User> Hasher = new PasswordHasher<User>();
                            UserSubmission.Password = Hasher.HashPassword(UserSubmission, UserSubmission.Password);
                            dbContext.Add(UserSubmission);
                            dbContext.SaveChanges();
                            HttpContext.Session.SetInt32("UserId", UserSubmission.UserId);
                            return RedirectToAction("dashboard","DojoActivity");
                        }
                }
                System.Console.WriteLine("******************************************** ModelState is NOT valid ********************************************");
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Index");
                } 

        /////////////////////////////////// POST LOGIN /////////////////////////////

            [HttpPost]
            [Route("loginpost")]
            public IActionResult LoginPost(LoginUser UserSubmission)
            {
            System.Console.WriteLine("*************************** Enter Registration Function ***************************");
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.User.FirstOrDefault(u => u.Email == UserSubmission.Email);
                if(userInDb == null)
                {
                    System.Console.WriteLine("*************************** Email not in Database error ***************************");
                    ModelState.AddModelError("Email", "Email not in Database");
                    return View("Index");
                }            
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(UserSubmission, userInDb.Password, UserSubmission.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Password","Wrong Password!");
                    return View("Index");
                }
                    System.Console.WriteLine("*************************** Success, Hashing password ***************************");
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("dashboard","DojoActivity");
            }
                ModelState.AddModelError("Email", "Invalid Email/Password");
                System.Console.WriteLine("*************************** Not Valid ***************************");
                return View("Index");
            }






    }
}

