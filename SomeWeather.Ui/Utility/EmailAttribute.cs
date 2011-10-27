using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SomeWeather.Core;
using StructureMap;

namespace SomeWeather.Ui.Utility
{
    public class EmailAttribute : ValidationAttribute
    {
        private readonly string _regEx;

        public EmailAttribute()
        {
            _regEx = ObjectFactory.GetInstance<IConfigurationService>().Get(Constants.Config.EmailRegEx);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var regEx = new Regex(_regEx);
            return regEx.IsMatch(value.ToString());
        }
    }
}