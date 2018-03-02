using LucidiaIT.Services;
using System.ComponentModel.DataAnnotations;

namespace LucidiaIT.Models.EmployeeModels
{
    public class Employee : Identifiable
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Initial image")]
        public string InitialImage { get; set; }

        [Required]
        [Display(Name = "Hover image")]
        public string HoverImage { get; set; }
    }
}
