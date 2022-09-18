using Microsoft.Extensions.DependencyInjection;
using YoutubeToMp3.Application.Services;

namespace YoutubeToMp3.Infrastructure.ExtensionMethods
{
    public static class ServicesExtensionMethod
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IDownloadService, DownloadService>();
        }
    }
}
