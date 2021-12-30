using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Api.Models
{
    public class BoligWebDbContext : IdentityDbContext
    {
        public BoligWebDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<DokumentViewModel> DokumentViewModel { get; set; }       

        public DbSet<BlogPostViewModel> BlogPostViewModel { get; set; }
        
    }
}
