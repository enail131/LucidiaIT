using Microsoft.EntityFrameworkCore;

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
