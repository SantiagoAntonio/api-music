using api_music.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_music
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<CD> CDs{ get; set; }

    }
}
