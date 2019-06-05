using Umbraco.Web.Mvc;
using System.Web.Mvc;
using BoulderProject_Umbraco.Models;
using System.Net.Mail;

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
            MailMessage message = new MailMessage("test@wohlan.de", model.EmailAddress);
            message.Subject = string.Format("Neue Anfrage von {0} {1} - {2}", model.FirstName, model.LastName, model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            client.Send(message);
        }
    }
}