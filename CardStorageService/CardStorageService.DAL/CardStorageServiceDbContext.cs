using Microsoft.EntityFrameworkCore;

namespace CardStorageService.DAL
{
    public class CardStorageServiceDbContext : DbContext
    {

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Card> Cards { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountSession> AccountSessions { get; set; }

        public CardStorageServiceDbContext(DbContextOptions options) : base(options) { }

    }
}
