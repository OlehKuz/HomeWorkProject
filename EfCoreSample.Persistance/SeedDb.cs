using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman;

namespace EfCoreSample.Persistance
{
    //TODO BUG IF have already 2 employees in DB(from config), then 2 enloyeedepartment have same keys
    public static class SeedDb
    {
        public static void Initialize(EfCoreSampleDbContext context)
        {
            if (!context.Projects.Any())
            {
                var projects = new List<Project>
                {
                    new Project
                    {
                        Title = "Title project1",
                        Status = "Pending",
                        StartTime = new DateTime(2019, 7, 30),
                        EndTime = new DateTime(2019, 8, 30)
                    },
                    new Project
                    {
                        Title = "Title project2",
                        Status = "Cancelled",
                        StartTime = new DateTime(2019, 7, 6),
                        EndTime = new DateTime(2019, 8, 25)
                    }

                };
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }
            if (!context.Departments.Any())
            {
                var departments = new List<Department>{
                    new Department()
                    {
                        Name = "UI department",
                        Location = "Milan"
                    },
                    new Department()
                    {
                        Name = "Back-end department",
                        Location = "Ternopil"
                    }
                };
                context.Departments.AddRange(departments);
                context.SaveChanges();
            }
            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        FirstName = "Roman",
                        LastName = "Tikhiy"
                    },
                    new Employee
                    {
                        FirstName = "Ivan",
                        LastName = "Hlabets"
                    },
                    new Employee
                    {
                        FirstName = "Igor",
                        LastName = "Sava"
                    }
                };
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

            var employeeId1 = long.MinValue;
            var employeeId2 = long.MinValue;
            var employeeId3 = long.MinValue;
            if (context.Employees.Any())
            {
                employeeId1 = context.Employees.First().Id;
                var employee = context.Employees.Skip(1).FirstOrDefault();
                if (employee != null) employeeId2 = employee.Id;
                else employeeId2 = employeeId1;
                employee = context.Employees.Skip(2).FirstOrDefault();
                if (employee != null) employeeId3 = employee.Id;
                else employeeId3 = employeeId2;
            }
            else if (!context.Employees.Any()) return;

            if (!context.Addresses.Any())
            {

                var addresses = new List<Address>
                {
                    new Address()
                    {
                        Street = "Lvivs`ka",
                        City = "Ternopil",
                        Country = "Ukraine",
                        EmployeeId = employeeId1
                    },
                    new Address()
                    {
                        Street = "Tarnavs`kogo",
                        City = "Ternopil",
                        Country = "Ukraine",
                        EmployeeId = employeeId2
                    },
                    new Address()
                    {
                        Street = "Rus`ka",
                        City = "Ternopil",
                        Country = "Ukraine",
                        EmployeeId = employeeId3
                    },

                };
                context.Addresses.AddRange(addresses);
                context.SaveChanges();
            }


            if (!context.EmployeeDepartments.Any())
            {
                if (context.Departments.Any())
                {
                    var departmentId1 = context.Departments.First().Id;
                    var departmentId2 = long.MinValue;

                    var department = context.Departments.Skip(1).FirstOrDefault();
                    if (department != null) departmentId2 = department.Id;
                    else departmentId2 = departmentId1;

                    var employeeDepartments = new List<EmployeeDepartment>
                    {
                        new EmployeeDepartment
                        {
                            EmployeeId = employeeId1,
                            DepartmentId = departmentId1
                        },
                        new EmployeeDepartment
                        {
                            EmployeeId = employeeId2,
                            DepartmentId = departmentId2
                        },
                        new EmployeeDepartment
                        {
                            EmployeeId = employeeId3,
                            DepartmentId = departmentId2
                        }
                    };
                    context.EmployeeDepartments.AddRange(employeeDepartments);
                    context.SaveChanges();
                }
            }

            if (!context.EmployeeProjects.Any())
            {
                if (context.Projects.Any())
                {
                    var projectId1 = context.Projects.First().Id;
                    var projectId2 = long.MinValue;

                    var project = context.Projects.Skip(1).FirstOrDefault();
                    if (project != null) projectId2 = project.Id;
                    else projectId2 = projectId1;

                    var employeeProjects = new List<EmployeeProject>
                    {
                        new EmployeeProject
                        {
                            EmployeeId = employeeId1,
                            ProjectId = projectId1

                        },
                        new EmployeeProject
                        {
                            EmployeeId = employeeId2,
                            ProjectId = projectId2
                        },
                        new EmployeeProject
                        {
                            EmployeeId = employeeId3,
                            ProjectId = projectId1
                        }
                    };
                    context.EmployeeProjects.AddRange(employeeProjects);
                    context.SaveChanges();
                }

            }

        }
    }
}
