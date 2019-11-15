using SAT.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SAT.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // GET
        public ActionResult Contact()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //check validation
            if (!ModelState.IsValid)
            {
                //send user back the form
                return View(cvm);
            }

            //email message we receive
            string message = $"Email from {cvm.Name} <br/>Email Address: {cvm.Email}<br/>Subject: {cvm.Subject}</br>Message:<br/>{cvm.Message}";

            //what sends the email
            MailMessage mm = new MailMessage("", null, message);

            //mail message properties
            //allow html formatting in email
            mm.IsBodyHtml = true;
            //high priority
            mm.Priority = MailPriority.High;
            //respond to sender's email
            mm.ReplyToList.Add(cvm.Email);

            //info from host - allow email to be sent
            SmtpClient client = new SmtpClient("");
            //client credentials
            client.Credentials = new NetworkCredential("");

            // try/catch
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We are sorry.  Your request could not be completed at this time.  Please try again later.<br/>Error Message: <br/>{ex.StackTrace}";
                return View(cvm);
            }
            //return confirmation to user
            return View("EmailConfirmation", cvm);
        }
    }
}
