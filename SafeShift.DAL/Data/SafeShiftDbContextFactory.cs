using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SafeShift.DAL.Data;

public class SafeShiftDbContextFactory : IDesignTimeDbContextFactory<SafeShiftDbContext>
{
    public SafeShiftDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SafeShiftDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=SafeShiftDB;User Id=SA;Password=Emmypee9;TrustServerCertificate=True;Encrypt=True;");

        return new SafeShiftDbContext(optionsBuilder.Options);
    }
}
