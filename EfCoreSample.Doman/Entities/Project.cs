using System;
using System.Collections.Generic;
using System.Text;
using EfCoreSample.Doman.Abstraction;
using EfCoreSample.Doman.DTO;

namespace EfCoreSample.Doman.Entities
{
    public class Project : IEntity<long>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //here status is a string because sql doesnt know projectstatus type
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
