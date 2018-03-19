using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Models.SolutionModels
{
    public class Solution : Identifiable
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
        [Display(Name = "Solution image")]
        public string SolutionImage { get; set; }       
    }
}
