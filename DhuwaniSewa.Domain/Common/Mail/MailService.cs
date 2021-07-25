using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class MailService : IMailService
    {
        public async  Task SendMailAsync(MailViewModel request)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(MailSetting.Email,"DhuwaniSewa");
                mailMessage.Subject = request.Subject;
                mailMessage.Body = request.Body;
                foreach(var to in request.To)
                {
                    mailMessage.To.Add(to);
                }
                var task = Task.Run(() => {
                    var credentials = MailSetting.AuthRequired ? 
                    new NetworkCredential(MailSetting.UserName, MailSetting.Password) : null;

                    SmtpClient client = new SmtpClient();
                    client.Host = MailSetting.ServerName;
                    client.Port = int.Parse(MailSetting.Port);
                    client.EnableSsl = MailSetting.UseSSL;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    string token = "Mail sent to:" + string.Join(",", request.To);
                    client.SendAsync(mailMessage,token);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                //Console.WriteLine("[{0}] Send canceled.", token);
                ///ToDO: Notify user that maile is canceld 
            }
            if (e.Error != null)
            {
                //Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
                ///ToDo: Notify user that some error has occured while sending mail
            }
            else
            {
                
                //Console.WriteLine("Message sent.");
                ///ToDo: Notify user that mail is sent successfully
            }
        }
    }
}
