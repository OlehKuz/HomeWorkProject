using EfCoreSample.Doman;
using EfCoreSample.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSample.Persistance.EntityConfiguration
{
    class EmployeeProjectEntityConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> employeeProjectBuilder)
        {
            employeeProjectBuilder.ToTable("employeeProjects", EfCoreSampleDbContext.SchemaName);

            employeeProjectBuilder.HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
            employeeProjectBuilder
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);

            employeeProjectBuilder
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);
        }
    }
}
