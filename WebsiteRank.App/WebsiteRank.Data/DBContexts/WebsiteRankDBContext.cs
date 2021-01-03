using Microsoft.EntityFrameworkCore;
using WebsiteRank.Domain;

namespace WebsiteRank.Data.DBContexts
{
    public class WebsiteRankDBContext : DbContext
    {
        public WebsiteRankDBContext(DbContextOptions<WebsiteRankDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SearchProviderType>()
                .HasData(new SearchProviderType { Id = 1, Name = "Google", Description = "Google search" },
                new SearchProviderType { Id = 2, Name = "Bing", Description = "Bing search" });                         
        }

        public DbSet<SearchHistory> SearchHistory { get; set; }

        public DbSet<SearchProviderType> SearchProviderType { get; set; }
    }
}
