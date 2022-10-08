using Converter.Services.Files;
using System.Runtime.CompilerServices;

namespace Converter.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
        }
    }
}
