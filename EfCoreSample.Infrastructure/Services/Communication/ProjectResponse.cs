using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreSample.Infrastructure.Services.Communication
{
    public class ProjectResponse:BaseResponse
    {
        public Project Project { get; private set; }

        private ProjectResponse(bool success, string message, Project project) : base(success, message)
        {
            Project = project;
        }

        public ProjectResponse(Project project) : this(true, string.Empty, project)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>

        public ProjectResponse(string message) : this(false, message, null)
        { }
    }
}
