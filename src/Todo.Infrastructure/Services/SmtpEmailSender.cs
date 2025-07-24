using MailKit.Net.Smtp;
using MimeKit;
using Todo.Application.Common.Interfaces;

namespace Todo.Infrastructure.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _cfg;
    public SmtpEmailSender(IConfiguration cfg) => _cfg = cfg;

    public async Task SendAsync(string to, string subject, string body)
    {
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress("Todo", _cfg["Smtp:From"]));
        msg.To.Add(new MailboxAddress(to, to));
        msg.Subject = subject;
        msg.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_cfg["Smtp:Host"], int.Parse(_cfg["Smtp:Port"]), false);
        await client.AuthenticateAsync(_cfg["Smtp:User"], _cfg["Smtp:Pass"]);
        await client.SendAsync(msg);
        await client.DisconnectAsync(true);
    }
}