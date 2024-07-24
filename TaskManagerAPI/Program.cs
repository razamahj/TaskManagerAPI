using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Services;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var builtConfig = config.Build();
                var environment = context.HostingEnvironment;
                // Load additional configuration settings based on environment if needed
            })
            .ConfigureServices((context, services) =>
            {
                // Configure DbContext with SQLite connection
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

                // Add services for repositories
                services.AddScoped<ITaskRepository, TaskRepository>();
                services.AddScoped<ISubTaskRepository, SubTaskRepository>();

                // Add services for business logic
                services.AddScoped<ITaskService, TaskService>();
                services.AddScoped<ISubTaskService, SubTaskService>();

                // Configure AutoMapper
                services.AddAutoMapper(typeof(Program));

                // Add controllers
                services.AddControllers();
            });
}