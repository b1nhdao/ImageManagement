using ImageManagement.Api.Services;
using ImageManagement.Domain.AggregatesModel.FileBaseAggregate;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Infrastructure;
using ImageManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ImageManagement.Api
{
    public static class ApplicationService
    {
        public static IHostApplicationBuilder AddApplicationService (this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IUploaderRepository, UploaderRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();

            builder.Services.AddScoped<IImageService, ImageService>();

            return builder;
        }
    }
}
