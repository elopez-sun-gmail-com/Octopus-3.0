using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UTIL
{
    /// <summary>
    /// @author ELH 
    /// </summary>
    public class Email
    {
        private static EmailParameter parameter { get; set; }

        private static void setParametros()
        {
            string emailConnection = System.Configuration.ConfigurationManager.AppSettings["EmailConnection"];

            emailConnection = emailConnection.Replace("'", "\"");

            Email.parameter = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailParameter>(emailConnection);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="memoryStream"></param>
        /// <param name="fileName"></param>
        public static void send(string to, string subject, string body, MemoryStream memoryStream, String fileName)
        {
            try
            {
                Email.setParametros();

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Email.parameter.SMTP);
                mail.From = new MailAddress(Email.parameter.USER);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                if (memoryStream != null)
                {
                    Attachment attachment = new Attachment(memoryStream, fileName);
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.Port = Convert.ToInt16(Email.parameter.PORT);
                SmtpServer.Credentials = new System.Net.NetworkCredential(Email.parameter.USER, Email.parameter.PASSWORD);
                SmtpServer.EnableSsl = true;

                // if (Setup.ENVIAR_EMAIL)
                {
                    SmtpServer.Send(mail);
                }

                Console.WriteLine("OK....");

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception -->> {0}  -->> {1}", e.InnerException, e.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachmentFile"></param>
        /// <param name="fileName"></param>
        public static void send(string to, string subject, string body, byte[] attachmentFile, String fileName)
        {
            try
            {
                Email.setParametros();

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Email.parameter.SMTP);
                mail.From = new MailAddress(Email.parameter.USER);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                if (attachmentFile != null)
                {
                    Attachment attachment = new Attachment(new MemoryStream(attachmentFile), fileName);
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.Port = Convert.ToInt16(Email.parameter.PORT);
                SmtpServer.Credentials = new System.Net.NetworkCredential(Email.parameter.USER, Email.parameter.PASSWORD);
                SmtpServer.EnableSsl = true;

                // if (Setup.ENVIAR_EMAIL)
                {
                    SmtpServer.Send(mail);
                }

                Console.WriteLine("OK....");

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception -->> {0}  -->> {1}", e.InnerException, e.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachmentFilename"></param>
        public static void send(string to, string subject, string body, string attachmentFilename)
        {

            try
            {
                Email.setParametros();

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.From = new MailAddress(Email.parameter.USER);
                mail.To.Add(to);
                //mail.Bcc.Add(new MailAddress(Email.EMAIL_CCO));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                Attachment data = null;

                if (attachmentFilename != null && attachmentFilename.Trim().Length > 0)
                {
                    data = new Attachment(attachmentFilename);

                    mail.Attachments.Add(data);

                }

                SmtpServer.Host = Email.parameter.SMTP;
                SmtpServer.Port = Convert.ToInt16(Email.parameter.PORT);
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Email.parameter.USER, Email.parameter.PASSWORD);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                // if (Setup.ENVIAR_EMAIL)
                {
                    SmtpServer.Send(mail);
                }

                if (data != null)
                {
                    data.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception -->> {0}  -->> {1}", e.InnerException, e.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="pathFile"></param>
        public static void send(string[] to, string subject, string body, string pathFile)
        {
            try
            {
                Email.setParametros();

                if (to != null && to.Length > 0)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(Email.parameter.SMTP);
                    mail.From = new MailAddress(Email.parameter.USER);

                    foreach (string item in to)
                    {
                        mail.To.Add(item);
                    }

                    mail.Subject = subject;
                    mail.Body = body;

                    if (pathFile != null && pathFile.Trim().Length > 0)
                    {
                        Attachment attachment = new Attachment(pathFile);
                        mail.Attachments.Add(attachment);
                    }

                    SmtpServer.Port = Convert.ToInt16(Email.parameter.PORT);
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Email.parameter.USER, Email.parameter.PASSWORD);
                    SmtpServer.EnableSsl = true;

                    // if (Setup.ENVIAR_EMAIL)
                    {
                        SmtpServer.Send(mail);

                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception -->> ", e.ToString());
            }

        }
    }

    public class EmailParameter
    {
        public string SMTP { get; set; }
        public string USER { get; set; }
        public string PASSWORD { get; set; }
        public string PORT { get; set; }
    }
}
