using ImageManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ImageManagement.Api
{
    public static class ApplicationService
    {
        public static IHostApplicationBuilder AddApplicationService (this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ImageDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            return builder;
        }
    }
}
