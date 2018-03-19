using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models.SolutionModels;

namespace LucidiaIT.Models
{
    public class SolutionContext : DbContext
    {
        public SolutionContext (DbContextOptions<SolutionContext> options)
            : base(options)
        {
        }

        public DbSet<LucidiaIT.Models.SolutionModels.Solution> Solution { get; set; }
    }
}
