﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace Janus.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        private readonly JanusEntities _context;

        public RegisterController()
        {
            _context = new JanusEntities();
        }

        public ActionResult Register()
        {
            var companyList = new SelectList(new[]
              {
                new { ID = "1", Name = "The Octave: Musical Instruments and Lessons" }
                
            },
              "ID", "Name", 1);

           ViewData["companylist"] = companyList;

            var questionList = new SelectList(new[]
              {
                new { ID = "1", Name = "What was your favorite Elementary School Teacher's Name?" },
                new { ID = "2", Name = "In What city did your mother and father meet?" },
                new { ID = "3", Name = "What is your youngest sibling's birthday?" },
                new { ID = "4", Name = "What is the first name of the boy or girl that you first kissed?" },
                new { ID = "5", Name = "What was the name of your first pet?" },
                new { ID = "6", Name = "What was your childhood nickname?" }

            },
              "ID", "Name", 1);

            ViewData["questionList"] = questionList;


            return View();
        }


        [HttpPost]
        public ActionResult CreateUser()
        {
            _context.Users.Add(new Users
            {
                companyID = 1,
                departmentID = 1,
                firstName = "Matthew",
                lastName = "Martin",
                birthDate = System.DateTime.Now,
                password = "test",
                phone = "289-244-3008",
                email = "mmartin7064@gmail.com",
                streetAddress = "257 Grosvenor Avenue North",
                postalCode = "L8L 7T2",
                hireDate = System.DateTime.Now,
                fireDate = System.DateTime.Now,
                employmentStatus = "Active"

            });
            _context.SaveChanges();


            return RedirectToAction("Login", "Login");
        }

    }

  
}