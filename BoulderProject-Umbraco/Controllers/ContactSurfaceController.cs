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
                var basicCredential = new NetworkCredential("system@boulder-point.de", "QWbeM7x9?6sT");

                var messageHtml = "";

                var header = "";

                if (model.Date !="")
                {
                    header = "<h2>Eine neue Geburtstags Anfrage ist eingegangen!<!h2></br></br>";
                }
                else
                {
                    header = "<h2>Eine neue Anfrage ist eingegangen!</h2></br></br>";
                }

                var htmlBody = "<p>Vorname: " + model.FirstName + "</p></br>" +
                               "<p>Nachname: " + model.LastName + " </p></br>" +
                               "<p>Emailadresse: " + model.EmailAddress + "</p></br>" +
                               "<p>Details: " + model.Message + "</p>";

                messageHtml = header + htmlBody;





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
                    message.Body = messageHtml;
                    message.To.Add("info@boulder-point.de");

                    try
                    {
                        smtpClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                        smtpClient.Send(message);
                    }
                    catch (Exception e)
                    {
                        Response.Write(e.Message);
                    }
                }
            }
            


            
        }
        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true || e.Error != null)
            {
                throw new Exception(e.Cancelled ? "EMail sedning was canceled." : "Error: " + e.Error.ToString());
            }
        }
    }
}