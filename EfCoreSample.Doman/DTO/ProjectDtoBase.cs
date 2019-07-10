using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public abstract class ProjectDtoBase
    {
        public string Title { get; set; }
        //define status property based on current time and starttime, endtime properties
        //in a service
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
