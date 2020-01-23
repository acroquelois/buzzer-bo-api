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

        public DbSet<Models.QuestionType> QuestionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionType>().HasData(
                new QuestionType { Id="TEXTE", Type="texte"},
                new QuestionType { Id = "AUDIO", Type = "audio" },
                new QuestionType { Id = "IMAGE", Type = "image" }
                );
        }
    }
}