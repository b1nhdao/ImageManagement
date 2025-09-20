using ImageManagement.Api.Services;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
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
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

            builder.Services.AddScoped<IFileStorageService, FileStorageService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            return builder;
        }
    }
}
