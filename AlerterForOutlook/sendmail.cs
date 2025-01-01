using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace WebTest
{
    class sendmail
    {

        public void sentOutlookMail(string mailadress, string subject, string station, float level)
        { 
        DateTime dt = DateTime.Now;
        string date = dt.ToString("dd.MM.yyyy hh:mm");

        Outlook.Application outlookApp = new Outlook.Application();
        Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

        // Set the properties of the mail item.
        mailItem.Subject = subject;
        mailItem.To = mailadress;
        mailItem.Body = dt + " Neckarpegel " + station + " ist " +level.ToString()+" cm";

        // Send the email.
        mailItem.Send();
        }
    }
}
