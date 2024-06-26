﻿using System.Net;
using System.Net.Mail;

namespace BadanieKrwi.Models
{
    public class EmailSender(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
    {
        private readonly string _smtpServer = smtpServer;
        private readonly int _smtpPort = smtpPort;
        private readonly string _senderEmail = senderEmail;
        private readonly string _senderPassword = senderPassword;

        public async void SendEmail(string recipientEmail, string subject, string body)
        {
            using SmtpClient client = new(_smtpServer, _smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
            client.EnableSsl = true;
            client.Timeout = 10000;

            MailMessage msg = new(_senderEmail, recipientEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(msg);
        }
    }
}