using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SentinelExceptionManagement
{
    public static class ExceptionManager
    {
        public static void LogException(Exception ex)
        {
            var fromAddress = new MailAddress("dave@daveajrussell.com", "Error Notifier");
            var toAddress = new MailAddress("dave@daveajrussell.com", "Admin");
            const string fromPassword = "randomness";
            const string subject = "Error in Sentinel";

            string strBody = string.Format("<table><tr><td><div style='color:#cc0000; font-size:26px; font-weight:bold'> Error: {0} </div><br />" +
                                           "<div> Inner Exception: {1} </div><br />" +
                                           "<div> Stack Trace: {2} <div><br />" +
                                           "<div> Target: {3} </div> <br /></td></tr></table>", ex.Message, ex.InnerException, ex.StackTrace, ex.TargetSite);

            var smtp = new SmtpClient
            {
                Host = "smtp.daveajrussell.com",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
                                 {  
                                     Priority = MailPriority.High,
                                     IsBodyHtml = true,
                                     Subject = subject,
                                     Body = strBody
                                 })
            {
                smtp.Send(message);
            }
        }
    }
}


