using System;

namespace EfCoreSample.Doman.DTO
{
    public abstract class ProjectDtoBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
