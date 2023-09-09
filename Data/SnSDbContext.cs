using Microsoft.EntityFrameworkCore;
using SnS.API.Model.Domain;

namespace SnS.API.Data
{
    public class SnSDbContext: DbContext
    {
        public SnSDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
            
        }

        public DbSet<Category>Categories{ get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
