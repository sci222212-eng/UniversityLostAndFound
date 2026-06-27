using Microsoft.EntityFrameworkCore;
using UniversityLostAndFound.Models;

namespace UniversityLostAndFound.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ItemReport> ItemReports { get; set; }
    }
}