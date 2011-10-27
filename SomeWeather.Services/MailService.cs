using System.Net.Mail;
using SomeWeather.Core;

namespace SomeWeather.Services
{
    public interface IMailService
    {
        void Send(string message);
    }

    public class MailService : IMailService
    {
        private readonly IConfigurationService _config;

        public MailService(IConfigurationService config)
        {
            _config = config;
        }

        public void Send(string message)
        {
            var toAddress = _config.Get(Constants.Config.AdminEmail);
            var email = new MailMessage(
                "mail@someweather.com", toAddress,
                "[SomeWeather.com] - Contact Form Submitted", message);

            var smtpServer = _config.Get(Constants.Config.SmtpServer);
            new SmtpClient(smtpServer).Send(email);
        }
    }
}