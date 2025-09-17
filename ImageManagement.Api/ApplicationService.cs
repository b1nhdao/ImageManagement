using ImageManagement.Api.Services;
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
            builder.Services.AddDbContext<ImageDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IUploaderRepository, UploaderRepository>();
            builder.Services.AddScoped<IImageServiceTest, ImageServiceTest>();

            builder.Services.AddScoped<IImageService, ImageService>();

            return builder;
        }
    }
}
