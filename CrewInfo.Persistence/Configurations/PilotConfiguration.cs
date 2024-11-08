using CrewInfo.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CrewInfo.Persistence.Configurations
{
    public class PilotConfiguration : IEntityTypeConfiguration<PilotEntity>
    {
        public void Configure(EntityTypeBuilder<PilotEntity> builder)
        {
            builder.HasKey(p => p.PilotId);

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.MobileNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.PassportNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.PassportIssueDate)
                .IsRequired();

            builder.Property(p => p.InnNumber)
                .HasMaxLength(12);

            builder.Property(p => p.InsurancePolicyNumber)
                .HasMaxLength(20);
        }
    }
}
