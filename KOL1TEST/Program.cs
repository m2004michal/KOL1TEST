using KOL1TEST.Middlewares;
using KOL1TEST.Repositories;
using KOL1TEST.Services;

namespace KOL1TEST;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        builder.Services.AddScoped<IDeliveriesService, DeliveriesService>();
        builder.Services.AddScoped<IDeliveriesRepository, DeliveriesRepository>();

        
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.UseGlobalExceptionHandling();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        
        app.Run();
    }
}