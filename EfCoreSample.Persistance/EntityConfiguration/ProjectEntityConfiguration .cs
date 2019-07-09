using System;
using System.Collections.Generic;
using System.Text;
using EfCoreSample.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSample.Persistance.EntityConfiguration
{
    class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> projectBuilder)
        {
            projectBuilder.ToTable("projects", EfCoreSampleDbContext.SchemaName);
            projectBuilder.HasKey(p => p.Id);

            projectBuilder.Property(p => p.Title).HasMaxLength(byte.MaxValue).IsRequired();
            projectBuilder.Property(p => p.Description).HasMaxLength(int.MaxValue);
            projectBuilder.Property(p => p.Status).HasMaxLength(10).IsRequired();
            projectBuilder.Property(p => p.LastUpdated)
                .HasDefaultValueSql("current_timestamp(6) ON UPDATE current_timestamp(6)")
                .ValueGeneratedOnAddOrUpdate();
            projectBuilder.Property(p => p.StartTime).IsRequired();

        }
    }
}
