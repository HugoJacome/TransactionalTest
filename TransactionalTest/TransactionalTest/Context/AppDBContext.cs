using Microsoft.EntityFrameworkCore;
using TransactionalTest.Interfaces;
using TransactionalTest.Models;

namespace TransactionalTest.Context
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Movements> Movements { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=UWBERMR4VR;Database=TransactionalDB;User ID=sa;Password=20bdd03.;");
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
