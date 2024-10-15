using CleanRazor.Web;
#if (SerilogLogging)
using Serilog;
#endif

#if (SerilogLogging)
try
{
#endif
    var builder = WebApplication.CreateBuilder(args);

#if (SerilogLogging)
    // Configure Serilog
    Logging.Configure(builder.Configuration);
#endif

    // Add services to the container.
#if (SerilogLogging)
    builder.Services.AddSerilog();
#endif
    builder.Services.AddApplication();
    builder.Services.AddEntityFrameworkCore(builder.Configuration);
    builder.Services.AddWebServices();
    builder.Services.AddRazorPages();

    // Build the application
    var app = builder.Build();
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

#if (SerilogLogging)
    app.UseSerilogRequestLogging();
#endif

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
#if (SerilogLogging)
}
#endif
#if (SerilogLogging)
catch (Exception ex)
{
    Log.Error(ex, "The host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
#endif