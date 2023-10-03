using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext>options):base(options)
        { 

        
        }
            
    }
}
