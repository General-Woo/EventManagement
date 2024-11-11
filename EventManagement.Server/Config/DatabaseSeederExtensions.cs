using EventManagementApp.Server.Core.Initializer;

namespace EventManagementApp.Server.Config
{
    public static class DatabaseSeederExtensions
    {
        public static IApplicationBuilder UseDatabaseSeeder(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var databaseSeeder = serviceScope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
                databaseSeeder.SeedDb();
            }

            return app;
        }
    }
}
