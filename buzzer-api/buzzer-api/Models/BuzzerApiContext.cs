using Microsoft.EntityFrameworkCore;

namespace buzzerApi.Models
{
    public class BuzzerApiContext : DbContext
    {
        public BuzzerApiContext(DbContextOptions<BuzzerApiContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Question> Question { get; set; }

        public DbSet<Models.User> User { get; set; }
    }
}