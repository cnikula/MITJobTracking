using MITJobTracker.Services.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace MITJobTracker.Services
{
    /// <summary>
    /// Provides application version information for the running assembly.
    /// The service determines the most appropriate version string by preferring
    /// <see cref="AssemblyInformationalVersionAttribute"/> (commonly used for semantic versions
    /// and metadata), falling back to the file version from <see cref="FileVersionInfo"/>,
    /// and finally the assembly version. If none are available, it returns "unknown".
    /// This read-only value is exposed via <see cref="IAppInfoService.Version"/> for use by
    /// UI, logging, and diagnostic components.
    /// </summary>
    public class AppInfoService : IAppInfoService
    {
        private readonly string _version;

        public AppInfoService()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            // Prefer AssemblyInformationalVersion (often contains semantic version + metadata)
            var informational = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            // Fall back to file version and assembly version
            string fileVersion = null;
            try
            {
                if (!string.IsNullOrEmpty(assembly.Location))
                {
                    var fi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    fileVersion = fi?.FileVersion;
                }
            }
            catch
            {
                // ignore - some runtime contexts don't expose Location
            }

            var assemblyVersion = assembly.GetName().Version?.ToString();

            _version = informational ?? fileVersion ?? assemblyVersion ?? "unknown";

          
            if (!string.IsNullOrEmpty(informational) && informational.Contains('+'))
            {
                informational = informational.Split('+')[0];
            }
            _version = informational;
        }

        public string Version => _version;
    }
}
