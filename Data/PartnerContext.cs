using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models.PartnerModels;

namespace LucidiaIT.Models
{
    public class PartnerContext : DbContext
    {
        public PartnerContext (DbContextOptions<PartnerContext> options)
            : base(options)
        {
        }

        public DbSet<LucidiaIT.Models.PartnerModels.Partner> Partner { get; set; }
    }
}
