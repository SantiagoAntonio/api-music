using api_music.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_music
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {   
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CdMembers>()
                .HasKey(x => new { x.MemberId, x.CDId });
            modelBuilder.Entity<CdGenders>()
                .HasKey(x => new { x.GenderId, x.CDId });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<CD> CDs{ get; set; }
        public DbSet<CdGenders> CDGenders { get; set; }
        public DbSet<CdMembers> CdMembers { get; set; }


    }
}
