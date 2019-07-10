using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public class ProjectPutDto : ProjectDtoBase
    {
        public long Id { get; set; }
        
        public EProjectStatus? Status { get; set; }     
       
    }
}
