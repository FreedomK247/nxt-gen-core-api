using MimeKit;
using MailKit.Net.Smtp;
using NxtGen.Account.API.BusinessLogic.Contracts;
using NxtGen.Account.API.BusinessLogic.Helpers;
using NxtGen.Account.API.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NxtGen.Account.API.BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(EmailConfiguration emailConfiguration, ILogger<EmailService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _logger = logger;
        }

        public void SendEmail(EmailMessageViewModel message)
        {
            var emailMessage = CreateMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(EmailMessageViewModel message)
        {
            var emailMessage = CreateMessage(message);

            await SendAsync(emailMessage);
        }

        private MimeMessage CreateMessage(EmailMessageViewModel message)
        {
            var emailMessage = new MimeMessage();
            var emailAddresses = new List<MailboxAddress>();
            emailAddresses.AddRange(message.To.Select(x => new MailboxAddress(address: x)));
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.EmailFrom));
            emailMessage.To.AddRange(emailAddresses);
            emailMessage.Subject = message.Subject;

            // HTML Body
            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:black;'>{0}</h2>", message.Content) };

            // only add if attachments are there.
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;

                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPass);

                    client.Send(mailMessage);
                }
                catch (Exception e)
                {
                    // TODO: log an error message or throw an exception, or both.
                    _logger.LogError(e.Message);
                    throw;
                }
                // to prevent any memory leaks : Dispose this service and disconnect PLEASE
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPass);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception e)
                {
                    // TODO: log an error message or throw an exception, or both.                    
                    _logger.LogError(e.Message);
                    throw;
                }
                // to prevent any memory leaks : Dispose this service and disconnect PLEASE
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
