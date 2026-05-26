using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace IngressoJa.Data.dbContext;

public class IngressoJaContextFactory : IDesignTimeDbContextFactory<IngressoJaContext>
{
    public IngressoJaContext CreateDbContext(string[] args)
    {
        Env.Load();

        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "ingressoja";
        var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "ingressoja";

        var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

        var optionsBuilder = new DbContextOptionsBuilder<IngressoJaContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());

        return new IngressoJaContext(optionsBuilder.Options);
    }
}