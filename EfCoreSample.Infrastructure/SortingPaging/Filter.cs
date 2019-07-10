using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EfCoreSample.Doman;

namespace EfCoreSample.Infrastructure.SortingPaging
{
    public static class Filter
    {
       
        public static IEnumerable<Project> GetFiltered(this IQueryable<Project> projects,
            string status, string title, string startTime, string endTime)
        {
            if (status != null) projects = projects.Where(s => s.Status.Equals(status));
            if (title != null) projects = projects.Where(s => s.Title.Equals(title));
            if (startTime != null) projects = projects.Where(s => s.StartTime.CompareTo(startTime) == 0);
            if (endTime != null) projects = projects.Where(s => s.EndTime.Date.CompareTo(endTime) == 0);
            return projects.AsEnumerable();
        }
    }
}
