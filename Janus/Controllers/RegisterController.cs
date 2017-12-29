﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Mvc;

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
            return View();
        }

        public ActionResult ReigstrationConfirmed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser()
        {
            //Collect Basic Info
            string formValidationErrors = "";
            Boolean formHasErrors = false;
            int retrievedID = 0;
            string firstName = Request["firstName"];
            string lastName = Request["lastName"];
            string streetAddress = Request["streetAddress"];
            string postalCode = Request["postalCode"];
            string department = Request["departmentList"];
            string unhashedPass = Request["password"];
            string unhasedConfirmedPass = Request["confirmpassword"];
            string bday = Request["birthDate"];
            string phone = Request["phone"];
            string email = Request["email"];
            string role = Request["roles"];
            string userQuestion = Request["questionList"];
            string userAnswer = Request["securityAnswer"];

            //Collect Availibility
            string sundayFrom = Request["sundayFrom"];
            string sundayTo = Request["sundayTo"];
            string mondayFrom = Request["mondayFrom"];
            string mondayTo = Request["mondayTo"];
            string tuesdayFrom = Request["tuesdayFrom"];
            string tuesdayTo = Request["tuesdayTo"];
            string wendnesdayFrom = Request["wednesdayFrom"];
            string wednesdayTo = Request["wednesdayTo"];
            string thursdayFrom = Request["thursdayFrom"];
            string thursdayTo = Request["thursdayTo"];
            string fridayFrom = Request["fridayFrom"];
            string fridayTo = Request["fridayTo"];
            string saturdayFrom = Request["saturdayFrom"];
            string saturdayTo = Request["saturdayTo"];

            //validate the users inputs
            if (!unhasedConfirmedPass.Equals(unhashedPass))
            {
                formValidationErrors += "/nPasswords Do Not Match";
                formHasErrors = true;
            }
            if (!email.Contains("@"))
            {
                formValidationErrors += "/nEmail is Invalid";

                formHasErrors = true;
            }
            string postalRegex = "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]";
            var postalMatch = Regex.Match(postalCode, postalRegex, RegexOptions.IgnoreCase);
            if (!postalMatch.Success)
            {
                formValidationErrors += "\n Postal Code Invalid";

                formHasErrors = true;
            }
            string phoneRegex = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";
            var phoneMatch = Regex.Match(phone, phoneRegex, RegexOptions.IgnoreCase);
            if (!phoneMatch.Success)
            {
                formValidationErrors += "\n Phone is Invalid";
                formHasErrors = true;
            }
            string birthdayRegex = @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$";
            var birthdayMatch = Regex.Match(bday, birthdayRegex, RegexOptions.IgnoreCase);
            if (!birthdayMatch.Success)
            {
                formValidationErrors += "\n Birthday is not in format dd/mm/yyyy";

                formHasErrors = true;
            }
            if (formHasErrors == true)
            {
                ViewBag.Error = formValidationErrors;
                return View("Register");
            }
            else
            {
                //Hash and salt the users password
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(unhashedPass, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                //add the user to the databae
                _context.Users.Add(new Users
                {
                    departmentName = department,
                    firstName = firstName,
                    lastName = lastName,
                    birthDate = bday,
                    password = savedPasswordHash,
                    phone = phone,
                    email = email,
                    streetAddress = streetAddress,
                    postalCode = postalCode,
                    role = role,
                    hireDate = System.DateTime.Now,
                    employmentStatus = "Active",
                    question = userQuestion,
                    userAnswer = userAnswer
                });
                _context.SaveChanges();

                //get the userID of the user registering
                var query = from u in _context.Users
                            where u.email.Contains(email)
                            orderby u.email
                            select u;

                foreach (var user in query)
                {
                    retrievedID = user.userID;
                }

                //Add Availibility of the User to the database

                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Sunday",
                    startTime = Int32.Parse(sundayFrom),
                    endTime = Int32.Parse(sundayTo),
                });
                _context.SaveChanges();

                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Monday",
                    startTime = Int32.Parse(mondayFrom),
                    endTime = Int32.Parse(mondayTo),
                });
                _context.SaveChanges();
                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Tuesday",
                    startTime = Int32.Parse(tuesdayFrom),
                    endTime = Int32.Parse(tuesdayTo),
                });
                _context.SaveChanges();
                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Wednesday",
                    startTime = Int32.Parse(wendnesdayFrom),
                    endTime = Int32.Parse(wednesdayTo),
                });
                _context.SaveChanges();
                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Thursday",
                    startTime = Int32.Parse(thursdayFrom),
                    endTime = Int32.Parse(thursdayTo),
                });
                _context.SaveChanges();
                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Friday",
                    startTime = Int32.Parse(fridayFrom),
                    endTime = Int32.Parse(fridayTo),
                });
                _context.SaveChanges();
                _context.Availibility.Add(new Availibility
                {
                    userID = retrievedID,
                    day = "Saturday",
                    startTime = Int32.Parse(saturdayFrom),
                    endTime = Int32.Parse(saturdayTo),
                });
                _context.SaveChanges();
            }
            return RedirectToAction("ReigstrationConfirmed", "Register");
        }
    }
}