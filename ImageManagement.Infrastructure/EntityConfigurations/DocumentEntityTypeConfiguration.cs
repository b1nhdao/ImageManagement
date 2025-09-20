using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageManagement.Infrastructure.EntityConfigurations
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("documents");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name);
            builder.Property(i => i.Url).IsRequired();
            builder.Property(i => i.UploadedTime);

            builder.Property(i => i.UploaderId).IsRequired();

            builder.HasOne<Uploader>()
                   .WithMany()
                   .HasForeignKey(i => i.UploaderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
