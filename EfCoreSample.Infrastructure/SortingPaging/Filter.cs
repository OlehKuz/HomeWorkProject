using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreSample.Infrastructure.SortingPaging
{
    public static class Filter
    {

        public static IEnumerable<Project> GetFiltered(this IQueryable<Project> projects,
            string status, string title, string startTime, string endTime)
        {
            if (status != null) projects = projects
                    .Where(s => s.Status.Equals(status));
            if (title != null) projects = projects
                    .Where(s => s.Title.Equals(title));
            if (startTime != null) projects = projects
                    .Where(s => s.StartTime.Date.CompareTo(Convert.ToDateTime(startTime)) == 0);
            if (endTime != null) projects = projects
                    .Where(s => s.EndTime.Date.CompareTo(Convert.ToDateTime(endTime)) == 0);
            return projects.AsEnumerable();
        }

        public static void GetSorted(this IEnumerable<Project> projects, string sort)
        {
            switch (sort)
            {
                case "update_desc":
                    projects.OrderByDescending(s => s.LastUpdated);
                    break;
                default:
                    projects.OrderBy(s => s.LastUpdated);
                    break;
            }
        }
        public static List<Project> GetPaginated(this IEnumerable<Project> projects, int? pageSize, int? pageNumber )
        {
            if (pageSize != null || pageNumber != null)
            {
                return PaginatedList<Project>.Create(projects, pageNumber ?? 1, pageSize ?? 2);
            }
            return projects.ToList();
        }
    }
}
