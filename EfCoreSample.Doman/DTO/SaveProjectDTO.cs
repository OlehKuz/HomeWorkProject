using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public class SaveProjectDTO
    {
        public string Title { get; set; }
        //define status property based on current time and starttime, endtime properties
        //in a service

        public EProjectStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
