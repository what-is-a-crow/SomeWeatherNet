using System.IO;
using System.Web;
using RazorEngine;
using SomeWeather.Ui.Models;

namespace SomeWeather.Ui.Utility
{
    public class MessageTemplate
    {
        private static string _template;

        public static string Expand(ContactModel model)
        {
            if (_template == null)
                _template = LoadTemplate();

            return Razor.Parse(_template, model);
        }

        private static string LoadTemplate()
        {
            var templatePath = HttpContext.Current.Server.MapPath("~/Content/MessageTemplate.cshtml");

            using (var s = File.Open(templatePath, FileMode.Open))
                return new StreamReader(s).ReadToEnd();
        }
    }
}