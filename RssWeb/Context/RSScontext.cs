using RssWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RssWeb.Context
{
    public class RSScontext : DbContext
    {
        public RSScontext()
            : base("DefaultConnection")
        { }

        public DbSet<RSS> RSSs { get; set; }

        public DbSet<RssSource> RssSources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RSS>().HasKey(r => new { r.Headline, r.Date });
            base.OnModelCreating(modelBuilder);
        }
    }
}
