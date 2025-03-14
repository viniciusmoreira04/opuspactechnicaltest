using Microsoft.EntityFrameworkCore;
using Opuspac.TechnicalTest.Portal.Server.Model;

namespace Opuspac.TechnicalTest.Portal.Server.Database;

public class OpuspacDatabaseContext : DbContext
{
    public OpuspacDatabaseContext(DbContextOptions<OpuspacDatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; protected set; }

    //public DbSet<RefreshToken> RefreshToken { get; protected set; }

    //public DbSet<UserToken> UserToken { get; protected set; }
}