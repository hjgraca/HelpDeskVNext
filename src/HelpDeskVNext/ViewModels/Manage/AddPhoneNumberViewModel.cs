using System.ComponentModel.DataAnnotations;

namespace HelpDeskVNext.ViewModels.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }
    }
}
