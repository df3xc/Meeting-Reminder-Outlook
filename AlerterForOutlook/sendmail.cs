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

        public void sentOutlookMail(string subject, string station, float level)
        { 
        Outlook.Application outlookApp = new Outlook.Application();
        Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

        // Set the properties of the mail item.
        mailItem.Subject = subject;
        mailItem.To = "df3xc@web.de";
        mailItem.Body = "Neckarpegel " + station + " ist "+level.ToString()+" cm";

        // Send the email.
        mailItem.Send();
        }
    }
}
