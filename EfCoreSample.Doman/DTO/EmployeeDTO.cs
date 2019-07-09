using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public class EmployeeDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public Employee ReportsTo { get; set; }

        public DateTime LastModified { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
