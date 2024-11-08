using CrewInfo.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CrewInfo.Persistence.Configurations
{
    internal class StewardConfiguration : IEntityTypeConfiguration<StewardEntity>
    {
        public void Configure(EntityTypeBuilder<StewardEntity> builder)
        {
            builder.HasKey(s => s.StewardId);

            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.MobileNumber)
               .IsRequired()
               .HasMaxLength(15);

            builder.Property(s => s.PassportNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(s => s.PassportIssueDate)
                .IsRequired();

            builder.Property(s => s.InnNumber)
                .HasMaxLength(12);

            builder.Property(s => s.InsurancePolicyNumber)
                .HasMaxLength(20);
        }
    }
}
