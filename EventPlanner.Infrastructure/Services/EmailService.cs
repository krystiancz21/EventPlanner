using EventPlanner.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EventPlanner.Infrastructure.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(configuration["EmailSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(configuration["EmailSettings:SmtpServer"], int.Parse(configuration["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);
        smtp.Authenticate(configuration["EmailSettings:SenderEmail"], configuration["EmailSettings:SenderPassword"]);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
