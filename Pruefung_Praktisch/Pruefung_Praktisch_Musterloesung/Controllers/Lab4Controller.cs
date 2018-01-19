using System;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Pruefung_Praktisch_Musterloesung.Models;
using System.Net.Mail;

namespace Pruefung_Praktisch_Musterloesung.Controllers
{
    public class Lab4Controller : Controller
    {

        /**
        * 
        * ANTWORTEN BITTE HIER
        * 
        * 2: Es ist interessant, zu schauen, welche Eingaben oft falsch gemacht werden. Von welcher IP und welchem Browser. Mögliche Hacker-IPs können gebannt werden
        * 
        * */

        public ActionResult Index()
        {

            Lab4IntrusionLog model = new Lab4IntrusionLog();
            return View(model.getAllData());
        }

        [HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];

            var emailaddress = Request["email"];

            bool intrusion_detected = false;

            // Hints
            var browser = Request.Browser.Platform;
            var ip = Request.UserHostAddress;

            Lab4IntrusionLog model = new Lab4IntrusionLog();

            // Hint:

            if (!IsEmailValid(emailaddress))
            {
                model.logIntrusion(ip, browser, "Email is in a wrong format");
            }
            if (!IsPasswordValid(password))
            {
                model.logIntrusion(ip, browser, "Email is in a wrong format");
            }

            if (intrusion_detected)
            {
                return RedirectToAction("Index", "Lab4");
            }
            else
            {
                // check username and password
                // this does not have to be implemented!
                return RedirectToAction("Index", "Lab4");
            }
        }

        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress); // Check if valid email address format

                if (emailaddress.ToLower() == emailaddress) // Check if lowercase is same as input
                {
                    return true;
                }
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsPasswordValid(string password)
        {
            if (password.Length >= 10 && password.Length <= 20) // Check if 
            {
                if (password.ToLower() != password && password.ToUpper() != password)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}