using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DbHandler
{
    public class WpfListDbContext : DbContext
    {
        public DbSet<AplicationUserModel> AplicationUser {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            builder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "WpfList.sqlite")}");
        }
    }
}
