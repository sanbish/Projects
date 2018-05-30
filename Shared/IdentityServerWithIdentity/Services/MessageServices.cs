using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace IdentityServerWithAspNetIdentity.Repositories
{
    
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly EmailConfig ec;
        private readonly SMSoptions so;

        public AuthMessageSender(IOptions<EmailConfig> emailConfig, IOptions<SMSoptions> smsOptions)
        {
            this.ec = emailConfig.Value;
            this.so = smsOptions.Value;
        }
        public async Task SendEmailAsync(String email, String subject, String message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = ec.LocalDomain;

                    await client.ConnectAsync(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.SslOnConnect).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential(ec.UserId, ec.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = so.accountSid;
            // Your Auth Token from twilio.com/console
            var authToken = so.authToken;

            TwilioClient.Init(accountSid, authToken);

            var msg = MessageResource.Create(
              to: new PhoneNumber(number),
              from: new PhoneNumber("+16366141210"), //"+15005550006"), test number
              body: message);

            return Task.FromResult(0);
        }
    }

    public class EmailConfig
    {
        public String FromName { get; set; }
        public String FromAddress { get; set; }

        public String LocalDomain { get; set; }

        public String MailServerAddress { get; set; }
        public String MailServerPort { get; set; }

        public String UserId { get; set; }
        public String UserPassword { get; set; }
    }

    public class SMSoptions
    {
        public string accountSid { get; set; }
        public string authToken { get; set; }
    }
}
