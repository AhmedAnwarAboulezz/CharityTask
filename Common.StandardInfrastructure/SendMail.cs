using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Common.StandardInfrastructure
{
    public class SendMail : ISendMail
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostingEnvironment;
        public SendMail(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task Send(string mailTo, string body, string subject, bool supportHtml = false)
        {
            try
            {
                Log.Information($"email to {mailTo}");
                var value = _configuration["Email:Smtp:EnableSSL"];
                var enableSsl = bool.Parse(value);
                var port = int.Parse(_configuration["Email:Smtp:Port"]);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["Email:Smtp:FromName"], _configuration["Email:Smtp:Username"]));
                message.To.Add(new MailboxAddress(mailTo, mailTo));
                message.Subject = subject;
                message.Body = new TextPart(supportHtml ? TextFormat.Html : TextFormat.Text) { Text = body };
                using var emailClient = new SmtpClient();
                emailClient.Connect(_configuration["Email:Smtp:Host"], port, enableSsl);
                emailClient.Authenticate(_configuration["Email:Smtp:Username"], _configuration["Email:Smtp:Password"]);
                await emailClient.SendAsync(message);
                emailClient.Disconnect(true);
            }
            catch (Exception e)
            {
                Log.Error(e, "email");
                Console.WriteLine(e);
                throw;
            }
        }
        public string GetBody(string body, bool isMobile = false)
        {
            var url = $"{(isMobile ? _configuration["FrontendUrlMobile"] : _configuration["FrontendUrl"])}reset?token={body}";
            var path = $"{_hostingEnvironment.ContentRootPath}/Templates/ResetPasswordTemplate.html";
            var htmlBody = File.ReadAllText(path);
            htmlBody = htmlBody.Replace("Urllogo", "https://drive.google.com/uc?export=view&id=1VAUJIA2YxqNsq6yE6y-GTm2nI6rKG5o-Tw");
            htmlBody = htmlBody.Replace("[ResetUrl]", url);
            return htmlBody;
        }
        public string GetWorkFlowBody(string nameAr, string nameEn, string bodyAr, string bodyEn, string url)
        {
            var baseUrl = _configuration["FrontendUrlMobile"] + url;
            var path = $"{_hostingEnvironment.ContentRootPath}/Templates/WorkFlow.html";
            var htmlBody = File.ReadAllText(path);
            htmlBody = htmlBody.Replace("$Urllogo$", "https://drive.google.com/uc?export=view&id=1VAUJIA2YxqNsq6yE6y-GTm2nI6rKG5o-Tw");
            htmlBody = htmlBody.Replace("$NameAr$", nameAr);
            htmlBody = htmlBody.Replace("$NameEn$", nameEn);
            htmlBody = htmlBody.Replace("$BodyAr$", bodyAr);
            htmlBody = htmlBody.Replace("$BodyEn$", bodyEn);
            htmlBody = htmlBody.Replace("$URL$", baseUrl);
            return htmlBody;
        }
    }
}
