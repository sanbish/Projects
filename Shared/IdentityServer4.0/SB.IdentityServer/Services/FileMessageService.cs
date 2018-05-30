using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace IdentityServerWithAspNetIdentity.Repositories
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
    public class FileMessageService : IMessageService
    {
        Task IMessageService.Send(string email, string subject, string message)
        {
            var emailMessage = $"To: {email}\nSubject: {subject}\nMessage: {message}\n\n";

            File.AppendAllText("emails.txt", emailMessage);

            return Task.FromResult(0);
        }
    }
}
