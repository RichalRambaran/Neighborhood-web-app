using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Buurtapp.Services
{
    public class EmailSender : IEmailSender
    {
        public string apiKey = "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";
        public EmailSender(IOptions<AuthMessageSenderOption> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOption Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(apiKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("19005830@student.hhs.nl", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
