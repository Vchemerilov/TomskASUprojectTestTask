using Microsoft.EntityFrameworkCore;

namespace ASUtestTask.Models
{
    public class PersonContext : DbContext, IContext
    {
        public DbSet<Person> persons { get; set; }

        public DbSet<Skill> skills { get; set; }

        public PersonContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Personsdb;Trusted_Connection=True;");
        }

        int IContext.SaveChanges()
        {
           return SaveChanges();
        }
    }
}
