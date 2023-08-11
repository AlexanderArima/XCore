using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class EmailMessage
    {
        public MailboxAddress Sender { get; set; }

        public MailboxAddress Reciever { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 创建一个邮件.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }

        public string Get()
        {
            var _notificationMetadata = GlobalVariable.EmailModel;
            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress("Self", _notificationMetadata.Sender);
            message.Reciever = new MailboxAddress("Self", _notificationMetadata.Reciever);
            message.Subject = "Welcome";
            message.Content = "Hello World!";
            var mimeMessage = CreateMimeMessageFromEmailMessage(message);
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_notificationMetadata.SmtpServer,
                _notificationMetadata.Port, true);
                smtpClient.Authenticate(_notificationMetadata.UserName,
                _notificationMetadata.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }

            return "Email sent successfully";
        }
    }
}
