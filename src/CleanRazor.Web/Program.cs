using CleanRazor.Web;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    // Configure Serilog
    Logging.Configure(builder.Configuration);

    // Add services to the container.
    builder.Services.AddSerilog();
    builder.Services.AddApplication();
    builder.Services.AddEntityFrameworkCore(builder.Configuration);
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

    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "The host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}