using System;
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
        public ActionResult CreateAccount(string firstName, string lastName, string streetAddress, string postalCode, string company, string password,string question, string answer)

        {
            string fname = firstName;
            string lname = lastName;
            string addr = streetAddress;
            string pcode = postalCode;
            string comp = company;
            string unhashedPass = password;
            string secQuestion = question;
            string qAnswer = answer;


            return RedirectToAction("Login", "Login");
        }
       

    }
}