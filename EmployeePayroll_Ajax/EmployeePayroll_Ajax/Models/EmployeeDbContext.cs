using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll_Ajax.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options):base(options)
        {

        }

        public DbSet<EmployeeModel> Employee { get; set; }
    }
}
