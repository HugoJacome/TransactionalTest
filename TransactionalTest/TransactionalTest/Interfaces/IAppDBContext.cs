using Microsoft.EntityFrameworkCore;
using TransactionalTest.Models;

namespace TransactionalTest.Interfaces
{
    public interface IAppDBContext
    {
        DbSet<Account> Account { get; set; }
        DbSet<Client> Client { get; set; }
        DbSet<Movements> Movements { get; set; }
        DbSet<Person> Person { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}