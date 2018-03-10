using System;
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

        private string _url;
        [Required]
        [Display(Name = "Company URL")]
        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                if ((value.StartsWith("http://")) || (value.StartsWith("https://")))
                {
                    _url = value;
                }
                else
                {
                    _url = $"http://{value}";
                }
            }
        }

        [Required]
        [Display(Name = "Company logo")]
        public string Logo { get; set; }
    }
}
