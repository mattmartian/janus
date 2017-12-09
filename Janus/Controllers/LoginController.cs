using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class LoginController : Controller
    {
        private readonly JanusEntities _context;
        // GET: Login

        public LoginController()
        {
            _context = new JanusEntities();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["userID"] = "";
            Session["email"] = "";
            Session["FirstName"] = "";
            Session["LastName"] = "";
            Session["accessLevel"] = "";

            return RedirectToAction("Login", "Login");
        }

        public ActionResult LoginUser()
        {
            //For login password https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129

            string email = Request["email"];

            string password = Request["password"];
            string userFirstName = "";
            string userLastName = "";
            int userID = 0;
            string userRole = "";

            string result = "";
            string retrievedEmail = "";
            string retrievedPassword = "";
            var query = from u in _context.Users
                        where u.email.Contains(email)
                        orderby u.email
                        select u;

            foreach (var user in query)
            {
                retrievedEmail = user.email;
                retrievedPassword = user.password;
            }

            if (string.IsNullOrEmpty(retrievedEmail))
            {
                ViewBag.Error = "Cannot Find an Account with email: " + email;
                return View("Login");
            }

            if (email.Equals(retrievedEmail))
            {
                // Fetch the stored value
                string savedPasswordHash = retrievedPassword;
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
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        ViewBag.Error = "Incorrect Password";
                        return View("Login");
                    }
                    else
                    {
                        result = "CORRECT PASSWORD";
                    }
                }
            }
            else
            {
                ViewBag.Error = "Email is Incorrect";
                return View("Login");
            }

            var details = from u in _context.Users
                          where u.email.Contains(email)
                          orderby u.userID
                          select u;

            foreach (var user in details)
            {
                userID = user.userID;
                userFirstName = user.firstName;
                userLastName = user.lastName;
                userRole = user.role;
            }

            Session["userID"] = userID.ToString();
            Session["email"] = email;
            Session["name"] = userFirstName + " " + userLastName;

            Session["accessLevel"] = userRole;

            return RedirectToAction("Welcome", "UserDashboard");
        }
    }
}