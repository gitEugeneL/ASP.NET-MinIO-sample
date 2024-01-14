namespace Infrastructure.Persistence;

public static class AppDbContextInitializer
{
    public static void Init(AppDbContext context) => context.Database.EnsureCreated();
}
