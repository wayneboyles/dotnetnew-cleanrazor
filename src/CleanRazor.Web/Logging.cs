using Serilog;
using Serilog.Events;

namespace CleanRazor.Web
{
    internal static class Logging
    {
        internal const string LogFile = "Logs/Log-.txt";

        internal static void Configure(IConfiguration configuration)
        {
            var config = new LoggerConfiguration();

            // Pull the configuration section
            var loggingSection = configuration.GetSection("Logging:LogLevel");

            // Set the minimum level
            var defaultLoggingLevel = loggingSection["Default"] ?? "Debug";
            config.MinimumLevel.Is(GetLogEventLevel(defaultLoggingLevel));

            // Add the overrides
            foreach (var source in loggingSection.GetChildren())
            {
                if (source.Key.Equals("Default", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                config.MinimumLevel.Override(source.Key, GetLogEventLevel(source.Value ?? "Warning"));
            }

            // Write To Log File
            config.WriteTo.Async(x => x.File(LogFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31));

            // Write to Debug & Console
#if DEBUG
            config.WriteTo.Async(x => x.Debug());
            config.WriteTo.Async(x => x.Console());
#endif

            // Add Enrichers
            config.Enrich.FromLogContext();
            config.Enrich.WithClientIp();
            config.Enrich.WithCorrelationId();

            // Build
            Log.Logger = config.CreateLogger();
        }

        private static LogEventLevel GetLogEventLevel(string level)
        {
            return Enum.Parse<LogEventLevel>(level, true);
        }
    }
}
