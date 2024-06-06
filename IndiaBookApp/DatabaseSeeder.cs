using IndiaBookApp.Data;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                    context.SeedDatabase();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ett fel inträffade när databasen seedades");
                }
            }
        }
    }
}
