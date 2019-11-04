using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace buzzerApi.Models
{
    public class BuzzerApiContext : DbContext
    {
        public BuzzerApiContext(DbContextOptions<BuzzerApiContext> options)
            : base(options)
        {
        }

        public DbSet<Models.QuestionTexte> QuestionTexte{ get; set; }

        public DbSet<Models.User> User { get; set; }
    }
}