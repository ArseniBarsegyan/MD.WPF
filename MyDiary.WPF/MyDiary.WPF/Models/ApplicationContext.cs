using System.Data.Entity;

namespace MyDiary.WPF.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
