using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
        public class EmailService : IEmailService
        {
            private readonly IConfiguration _configuration;

            public EmailService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public void SendEmail(AccountRegistrationDto account)
            {
                var smtpServer = _configuration["SmtpSettings:Server"];
                var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
                var smtpUsername = _configuration["SmtpSettings:Username"];
                var smtpPassword = _configuration["SmtpSettings:Password"];
                var senderEmail = _configuration["SmtpSettings:SenderEmail"];

                var emailMessage = CreateEmailMessage(account);

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpServer, smtpPort, useSsl: false);
                    client.Authenticate(smtpUsername, smtpPassword);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }

            private MimeMessage CreateEmailMessage(AccountRegistrationDto account)
            {
                var message = new MimeMessage();
                var senderName = "Your App Name"; // Možete ga dodati u appsettings.json ako se često menja

                message.From.Add(new MailboxAddress(senderName, _configuration["SmtpSettings:SenderEmail"]));
                message.To.Add(new MailboxAddress(account.Name, account.Email));
                message.Subject = "Verification Email";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<p>Dear {account.Name},</p>" +
                                      $"<p>Thank you for registering. Please click the following link to verify your email:</p>" +
                                      $"<a href='https://your-app.com/verify/{ account.VerificationToken }'>Verify Email</a>";

                message.Body = bodyBuilder.ToMessageBody();

                return message;
            }

            public void GenerateVerificationToken(AccountRegistrationDto account)
            {
                account.VerificationToken = Guid.NewGuid().ToString();
            }
        }
}
