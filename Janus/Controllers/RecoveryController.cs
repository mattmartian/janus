/*
 *I, Matthew Martin, student number 000338807, certify that this material is my original work.
    No other person's work has been used without due acknowledgement and I have not made my work available to anyone else.

    */

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace Janus.Controllers
{
    /// <summary>
    /// The RevoveryController is responsible for resetting the users password if they have forgotten it
    /// </summary>
    public class RecoveryController : Controller
    {
        private readonly JanusEntities _context;

        public RecoveryController()
        {
            //initialize the database context for the entire controller to use
            _context = new JanusEntities();
        }

        //Return the view to verify the user has an account
        public ActionResult VerifyAccount()
        {
            return View();
        }

        //Return the view to let the user know their password has been successfully reset
        public ActionResult PasswordChangedConfirmation()
        {
            return View();
        }

        //Display the users security question and await their asnwer before resetting the password
        public ActionResult SecurityQuestion()
        {
            //Collect the security question and answer for the user requesting a password reset
            string questionRecieved = "";

            string answerRecieved = "";

            string userEmail = TempData["userEmail"].ToString();

            var query = from u in _context.Users
                        where u.email.Contains(userEmail)
                        orderby u.email
                        select u;

            foreach (var user in query)
            {
                questionRecieved = user.question;
                answerRecieved = user.userAnswer;
            }

            ViewBag.Question = questionRecieved;

            TempData["userAnswer"] = answerRecieved;

            return View();
        }

        //Display the form for the user to enter in their new password
        public ActionResult ResetPassword()
        {
            return View();
        }

        /// <summary>
        /// Verify that the user has an account with the email they have entered
        /// </summary>
        /// <returns>Return the users security question and wait for their answer</returns>
        [HttpPost]
        public ActionResult VerifyEmail()
        {
            /**
             * Verify the email of the user to make sure they have an account, if they do then retrieve their seecurity question and answer
             **/
            string emailRetrieved = "";
            string userEmail = "";

            userEmail = Request["email"];

            var query = from u in _context.Users
                        where u.email.Contains(userEmail)
                        orderby u.email
                        select u;

            foreach (var user in query)
            {
                emailRetrieved = user.email;
            }

            //Validation
            if (string.IsNullOrEmpty(emailRetrieved))
            {
                ViewBag.Error = "Cannot Find an Account with email: " + userEmail;
                return View("VerifyAccount");
            }

            if (!userEmail.Equals(emailRetrieved))
            {
                ViewBag.Error = "Email is Incorrect";
                return View("Login");
            }
            else
            {
                TempData["userEmail"] = userEmail;
            }

            return RedirectToAction("SecurityQuestion");
        }

        /// <summary>
        /// The VerifyAnswer action will check if the users answer to the security question matches the one they had initally set on sign up
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifyAnswer()
        {
            //Check if the users answer is the correct answer for the security question
            String secAnswer = TempData["userAnswer"].ToString();

            String uiAnswer = Request["securityAnswer"];

            if (!uiAnswer.Equals(secAnswer))
            {
                ViewBag.Error = "Incorrect Answer";
                return View("Security Question");
            }

            return RedirectToAction("ResetPassword");
        }

        /// <summary>
        ///The PasswordReset action will collect the users new entered password, hash it and store it inside the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PasswordReset()
        {
            //Reset the password to the password supplied by the user
            string newpass = Request["newpassword"];
            string confirmnewpass = Request["confirmnewpassword"];
            string userEmail = Request["email"];

            if (!newpass.Equals(confirmnewpass))
            {
                ViewBag.Error = "Passwords Do Not Match";
                return View("ResetPassword");
            }

            try
            {
                //Hash and salt the new password

                //update the new password in the database
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(newpass, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                using (_context)
                {
                    var userFound = (from u in _context.Users
                                     where u.email == userEmail
                                     select u).FirstOrDefault();
                    userFound.password = savedPasswordHash;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Could not reset Password!";
            }

            return RedirectToAction("PasswordChangedConfirmation", "Recovery");
        }
    }
}