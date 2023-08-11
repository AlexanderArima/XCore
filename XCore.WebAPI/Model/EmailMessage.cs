using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    /// <summary>
    /// 电子邮件相关的类.
    /// </summary>
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

        /// <summary>
        /// 发送邮件.
        /// </summary>
        /// <param name="distEmail">邮件接收者.</param>
        /// <param name="code">验证码.</param>
        /// <returns></returns>
        public Tuple<bool, string> Send(string distEmail, string code)
        {
            var _notificationMetadata = GlobalVariable.EmailModel;
            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress("Microsoft", _notificationMetadata.Sender);
            message.Reciever = new MailboxAddress("", distEmail);
            message.Subject = "验证邮件主题";
            message.Content = "您好，验证码是：" + code + "，有效期为1分钟";
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

            return new Tuple<bool, string>(true, string.Empty);
        }
    }
}
