﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Models.EmployeeModels
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

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