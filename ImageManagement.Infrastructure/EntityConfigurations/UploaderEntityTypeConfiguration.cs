using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageManagement.Infrastructure.EntityConfigurations
{
    public class UploaderEntityTypeConfiguration : IEntityTypeConfiguration<Uploader>
    {
        public void Configure(EntityTypeBuilder<Uploader> builder)
        {
            builder.ToTable("uploader");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName);
        }
    }
}
