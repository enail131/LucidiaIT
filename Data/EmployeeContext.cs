using Microsoft.EntityFrameworkCore;

namespace LucidiaIT.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext (DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<LucidiaIT.Models.EmployeeModels.Employee> Employee { get; set; }
    }
}
