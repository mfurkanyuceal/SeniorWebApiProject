using System.ComponentModel.DataAnnotations;

namespace SeniorWepApiProject.ViewModels
{
    public class ResetPasswordModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parola onayla")]
        [Compare("Password",
            ErrorMessage = "Parola ve Parola onayla eşleşmiyor!")]
        public string ConfirmPassword { get; set; }
    }
}