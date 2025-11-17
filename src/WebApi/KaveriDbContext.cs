namespace Kaveri.WebApi;

using Microsoft.EntityFrameworkCore;

public class KaveriDbContext : DbContext
{
    public KaveriDbContext(DbContextOptions<KaveriDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
