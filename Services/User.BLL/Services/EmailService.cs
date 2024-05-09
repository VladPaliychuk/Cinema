using System.Net;
using System.Net.Mail;
using User.BLL.Services.Interfaces;

namespace User.BLL.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient = new("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("cozycinemaua@gmail.com", "cozycinemauapassword"),
        EnableSsl = true
    };

    private const string FromAddress = "cozycinemaua@gmail.com";

    public async Task SendEmailAsync(string toAddress, string subject, string body)
    {
        var mailMessage = new MailMessage(FromAddress, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email to {toAddress}: {ex.Message}");
        }
        finally
        {
            mailMessage.Dispose(); 
        }
    }
}