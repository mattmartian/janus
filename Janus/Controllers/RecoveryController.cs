using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class RecoveryController : Controller
    {
        private readonly JanusEntities _context;

        public RecoveryController()
        {
            _context = new JanusEntities();
        }

        // GET: Recovery
        public ActionResult VerifyAccount()
        {
            return View();
        }

        public ActionResult PasswordChangedConfirmation()
        {
            return View();
        }

        public ActionResult SecurityQuestion()
        {
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

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyEmail()
        {
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

        [HttpPost]
        public ActionResult VerifyAnswer()
        {
            String secAnswer = TempData["userAnswer"].ToString();

            String uiAnswer = Request["securityAnswer"];

            if (!uiAnswer.Equals(secAnswer))
            {
                ViewBag.Error = "Incorrect Answer";
                return View("Security Question");
            }

            return RedirectToAction("ResetPassword");
        }

        [HttpPost]
        public ActionResult PasswordReset()
        {
            string newpass = Request["newpassword"];
            string confirmnewpass = Request["confirmnewpassword"];
            string userEmail = Request["email"];

            if (!newpass.Equals(confirmnewpass))
            {
                ViewBag.Error = "Passwords Do Not Match";
                return View("ResetPassword");
            }

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

            return RedirectToAction("PasswordChangedConfirmation", "Recovery");
        }
    }
}