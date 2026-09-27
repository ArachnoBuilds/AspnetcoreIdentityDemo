namespace Api;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;" +
            "Port=5432;" +
            "Database=AspnetcoreIdentityDemo;" +
            "Username=postgres;" +
            "Password=E-{5V{xdRpUY8s9EX}YmUN");

        return new AppDbContext(optionsBuilder.Options);
    }
}
