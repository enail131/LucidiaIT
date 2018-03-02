using LucidiaIT.Services;
using System.ComponentModel.DataAnnotations;

namespace LucidiaIT.Models.PartnerModels
{
    public class Partner : Identifiable
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Company URL")]
        public string URL { get; set; }

        [Required]
        [Display(Name = "Company logo")]
        public string Logo { get; set; }
    }
}
