using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCoreSample.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "efcoresample");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                schema: "efcoresample",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: true),
                    ReportsToId = table.Column<long>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false, defaultValueSql: "current_timestamp(6) ON UPDATE current_timestamp(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employees_employees_ReportsToId",
                        column: x => x.ReportsToId,
                        principalSchema: "efcoresample",
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                schema: "efcoresample",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Status = table.Column<string>(maxLength: 10, nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "current_timestamp(6) ON UPDATE current_timestamp(6)"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "efcoresample",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_addresses_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "efcoresample",
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeDepartments",
                schema: "efcoresample",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeDepartments", x => new { x.EmployeeId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_employeeDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeDepartments_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "efcoresample",
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeProjects",
                schema: "efcoresample",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeProjects", x => new { x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_employeeProjects_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "efcoresample",
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeProjects_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "efcoresample",
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_EmployeeId",
                schema: "efcoresample",
                table: "addresses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_City_Country_Street",
                schema: "efcoresample",
                table: "addresses",
                columns: new[] { "City", "Country", "Street" })
                .Annotation("MySql:FullTextIndex", true);

            migrationBuilder.CreateIndex(
                name: "IX_employeeDepartments_DepartmentId",
                schema: "efcoresample",
                table: "employeeDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeProjects_ProjectId",
                schema: "efcoresample",
                table: "employeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_ReportsToId",
                schema: "efcoresample",
                table: "employees",
                column: "ReportsToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "efcoresample");

            migrationBuilder.DropTable(
                name: "employeeDepartments",
                schema: "efcoresample");

            migrationBuilder.DropTable(
                name: "employeeProjects",
                schema: "efcoresample");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "employees",
                schema: "efcoresample");

            migrationBuilder.DropTable(
                name: "projects",
                schema: "efcoresample");
        }
    }
}
