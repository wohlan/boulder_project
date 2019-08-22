using Umbraco.Web.Mvc;
using System.Web.Mvc;
using BoulderProject_Umbraco.Models;
using System.Net.Mail;
using System.Net;
using System;

namespace BoulderProject_Umbraco.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact/";
        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["ContactSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        private void SendEmail(ContactModel model)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential("anmeldung-test@boulder-point.de", "bulderadmin.pw");

                using (MailMessage message = new MailMessage())
                {
                    smtpClient.Host = "smtp.ionos.de";
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    message.From = new MailAddress(model.EmailAddress);
                    message.Subject = string.Format("Neue Anfrage von {0} {1} - {2}", model.FirstName, model.LastName, model.EmailAddress);
                    message.IsBodyHtml = true;
                    message.Body = model.Message;
                    message.To.Add("lennardwohlan@web.de");

                    try
                    {
                        smtpClient.Send(message);
                    }
                    catch (Exception e)
                    {
                        Response.Write(e.Message);
                    }
                }
            }
            


            
        }
    }
}