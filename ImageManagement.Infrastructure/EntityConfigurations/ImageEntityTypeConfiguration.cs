using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageManagement.Infrastructure.EntityConfigurations
{
    public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("images");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name);
            builder.Property(i => i.Url).IsRequired();
            builder.Property(i => i.UploadedTime);
            builder.OwnsOne(i => i.Demension);

            builder.Property(i => i.UploaderId).IsRequired();

            builder.HasOne<Uploader>()
                   .WithMany()
                   .HasForeignKey(i => i.UploaderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
