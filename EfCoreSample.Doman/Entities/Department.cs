using EfCoreSample.Doman.Abstraction;
using System.Collections.Generic;

namespace EfCoreSample.Doman
{
    public class Department : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
