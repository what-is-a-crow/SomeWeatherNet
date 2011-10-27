using System.ComponentModel.DataAnnotations;
using SomeWeather.Ui.Utility;

namespace SomeWeather.Ui.Models
{
    public class ContactModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string CompanyName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        [StringLength(2500)]
        public string Message { get; set; }
    }
}