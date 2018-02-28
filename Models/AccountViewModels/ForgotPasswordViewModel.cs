using System.ComponentModel.DataAnnotations;

namespace LucidiaIT.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
