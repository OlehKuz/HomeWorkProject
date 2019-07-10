using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public class ProjectGetDto: ProjectDtoBase
    {
        public long Id { get; set; }
        public string Status { get; set; }
    }
}
