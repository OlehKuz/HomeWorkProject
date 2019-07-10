using EfCoreSample.Doman;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSample.Persistance.EntityConfiguration
{
    class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employeeBuilder)
        {
            employeeBuilder.ToTable("employees", EfCoreSampleDbContext.SchemaName);

            employeeBuilder.HasKey(e => e.Id);

            employeeBuilder
                .HasMany(e => e.ReportsToEmployees)
                .WithOne(e => e.ReportsTo)
                .HasForeignKey(e => e.ReportsToId);

            employeeBuilder.Property(e => e.FirstName).HasMaxLength(128).IsRequired();
            employeeBuilder.Property(e => e.LastName).HasMaxLength(128);

            employeeBuilder.Property(t => t.LastModified)
                .HasDefaultValueSql("current_timestamp(6) ON UPDATE current_timestamp(6)")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
