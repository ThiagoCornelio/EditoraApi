using ProjetoEditoraApi;
using System.Net;
using System.Net.Mail;

namespace ProjetoEditoraApi.Services;

public class EmailService
{
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Enviado por Thiago",
        string fromEmail = "thiago@teste.com")
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port)
        {
            Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true
        };
        var mail = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mail.To.Add(new MailAddress(toEmail, toName));

        try
        {
            smtpClient.Send(mail);
            return true;
        }
        catch (Exception )
        {
            return false;
        }
    }
}