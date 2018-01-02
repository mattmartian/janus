using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Janus.Controllers
{
    /// <summary>
    /// The Register Controller will take care of all the functinality regarding creating a user
    /// </summary>
    public class RegisterController : Controller
    {
        //Set the database context globally
        private readonly JanusEntities _context;

        //Constructor for controller
        public RegisterController()
        {
            _context = new JanusEntities();
        }

        //Return the Register View
        public ActionResult Register()
        {
            return View();
        }

        //Confirm that the User has registered successfully
        public ActionResult ReigstrationConfirmed()
        {
            return View();
        }

        /// <summary>
        ///This create user action will take care of all the grunt work. it will collect all of the data the user has entereed about themself, create them as a user and
        ///store the users info and availibility inside the database
        /// </summary>
        /// <returns>A View to confirm that the user has registered successfully</returns>
        [HttpPost]
        public ActionResult CreateUser()
        {
            //Collect Basic Info
            string formValidationErrors = "";
            Boolean formHasErrors = false;
            int retrievedID = 0;
            string verification = "987651";
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
            string managerCode = Request["confirmManager"];

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

            /*
             * Check if the user already exists
             *
             * **/

            var getUser = (from a in _context.Users where a.email == email select a);
            var userCount = getUser.Count();
            if (userCount > 0)
            {
                formValidationErrors += "\n Already Registered";
                formHasErrors = true;
            }

            //validate the users inputs
            if (!managerCode.Equals(verification))
            {
                formValidationErrors += "\n Wrong Manager Verification";
                formHasErrors = true;
            }
            if (!unhasedConfirmedPass.Equals(unhashedPass))
            {
                formValidationErrors += "\n Passwords Do Not Match";
                formHasErrors = true;
            }
            if (!email.Contains("@"))
            {
                formValidationErrors += "\n Email is Invalid";

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

                string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                string[] startTime = { sundayFrom, mondayFrom, tuesdayFrom, wendnesdayFrom, thursdayFrom, fridayFrom, saturdayFrom };
                string[] endTime = { sundayTo, mondayTo, tuesdayTo, wednesdayTo, thursdayTo, fridayTo, saturdayTo };
                //Add Availibility of the User to the database
                for (int i = 0; i < 7; i++)
                {
                    _context.Availibility.Add(new Availibility
                    {
                        userID = retrievedID,
                        day = days[i],
                        startTime = Int32.Parse(startTime[i]),
                        endTime = Int32.Parse(endTime[i]),
                    });
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("ReigstrationConfirmed", "Register");
        }
    }
}