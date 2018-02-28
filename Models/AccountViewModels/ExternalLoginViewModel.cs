using System.ComponentModel.DataAnnotations;

namespace LucidiaIT.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
