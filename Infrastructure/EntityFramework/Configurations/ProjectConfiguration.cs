using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Priority)
            .HasConversion(x => (int) x, i => (ProjectPriorityEnum) i);

        builder.Property(x => x.Client)
            .IsRequired();

        builder.Property(x => x.ExecutiveCompanyName)
            .IsRequired();

        builder.HasOne(x => x.ProjectLead)
            .WithMany(x => x.LedProjects)
            .HasForeignKey(x => x.ProjectLeadId);
    }
}