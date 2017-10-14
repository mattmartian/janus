using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
<<<<<<< HEAD
  
=======
            var departmentList = new SelectList(new[]
              {
                new { ID = "1", Name = "Sales Floor" },
                new { ID = "2", Name = "Music Lessons" }


            },
              "ID", "Name", 1);

           ViewData["departmentList"] = departmentList;

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

            


>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
            return View();
        }







        [HttpPost]
        public ActionResult CreateUser()
        {
            string formValidationErrors = "";
            Boolean formHasErrors = false;

            string firstName = Request["firstName"];
            string lastName = Request["lastName"];
            string streetAddress = Request["streetAddress"];
            string postalCode = Request["postalCode"];
            int department = Int32.Parse(Request["departmentList"]);
            string unhashedPass = Request["password"];
            string unhasedConfirmedPass = Request["confirmpassword"];
            string bday = Request["birthDate"];
            string phone = Request["phone"];
            string email = Request["email"];
            string userQuestion = Request["questionList"];
            string userAnswer = Request["securityAnswer"];

<<<<<<< HEAD

            if (!unhasedConfirmedPass.Equals(unhashedPass))
            {
                formValidationErrors += "/nPasswords Do Not Match";
                formHasErrors = true;

=======
            
            if (!unhasedConfirmedPass.Equals(unhashedPass))
            {
                formValidationErrors += "Passwords Do Not Match";
                formHasErrors = true;
                
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
            }

            if (!email.Contains("@"))
            {
<<<<<<< HEAD
                formValidationErrors += "/nEmail is Invalid";
=======
                formValidationErrors += "Email is Invalid";
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
                formHasErrors = true;
            }

            string postalRegex = "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]";

            var postalMatch = Regex.Match(postalCode, postalRegex, RegexOptions.IgnoreCase);

            if (!postalMatch.Success)
            {
<<<<<<< HEAD
                formValidationErrors += "/nPostal Code Invalid";
=======
                formValidationErrors += "Postal Code Invalid";
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
                formHasErrors = true;
            }

            string phoneRegex = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";

            var phoneMatch = Regex.Match(phone, phoneRegex, RegexOptions.IgnoreCase);

            if (!phoneMatch.Success)
            {
<<<<<<< HEAD
                formValidationErrors += "/nPhone is Invalid";
                formHasErrors = true;
            }

            string birthdayRegex = @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$";
=======
                formValidationErrors += "Phone is Invalid";
                formHasErrors = true;
            }

            string birthdayRegex = @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01


            var birthdayMatch = Regex.Match(bday, birthdayRegex, RegexOptions.IgnoreCase);

            if (!birthdayMatch.Success)
            {
<<<<<<< HEAD
                formValidationErrors += "/nBirthday is not in format dd/mm/yyyy";
=======
                formValidationErrors += "Birthday is not in format dd/mm/yyyy";
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
                formHasErrors = true;
            }


            if (formHasErrors == true)
            {
<<<<<<< HEAD
                ViewBag.Error = formValidationErrors;
                return View("Register");
            }
            else
=======
                ViewBag.error = formValidationErrors;
                return View();
            } else
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(unhashedPass, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);



                //For login password https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
                /*
                // Fetch the stored value 
                string savedPasswordHash = DBContext.GetUser(u => u.UserName == user).Password;
                /// Extract the bytes 
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                //Get the salt 
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                // Compute the hash on the password the user entered 
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                // Compare the results 
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        throw new UnauthorizedAccessException();*/


                _context.Users.Add(new Users
                {
                    departmentID = department,
                    firstName = firstName,
                    lastName = lastName,
                    birthDate = bday,
                    password = savedPasswordHash,
                    phone = phone,
                    email = email,
                    streetAddress = streetAddress,
                    postalCode = postalCode,
                    hireDate = System.DateTime.Now,
                    employmentStatus = "Active",
                    question = userQuestion,
                    userAnswer = userAnswer

                });


<<<<<<< HEAD

                _context.SaveChanges();


                return RedirectToAction("Login", "Login");
            }
=======
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01

                _context.SaveChanges();

<<<<<<< HEAD
=======

                return RedirectToAction("Login", "Login");
            }

           
>>>>>>> 5cec28e86c3b3d38aec42b69aad4862d91c49c01
        }

    }


}