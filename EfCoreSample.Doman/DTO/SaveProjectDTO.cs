﻿using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public class ProjectPostDto : ProjectDtoBase
    {
        public EProjectStatus? Status { get; set; }
        
    }
}
