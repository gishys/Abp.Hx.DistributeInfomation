using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Hx.BgApp.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BgAppDbContextFactory : IDesignTimeDbContextFactory<BgAppDbContext>
{
    public BgAppDbContext CreateDbContext(string[] args)
    {
        BgAppEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        //var builder = new DbContextOptionsBuilder<BgAppDbContext>()
        //        .UseMySql(
        //        configuration.GetConnectionString("Default"),
        //        new MySqlServerVersion(new Version(5, 5, 60)));
        var builder = new DbContextOptionsBuilder<BgAppDbContext>()
        .UseNpgsql(configuration.GetConnectionString("Default"));

        return new BgAppDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Hx.BgApp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
