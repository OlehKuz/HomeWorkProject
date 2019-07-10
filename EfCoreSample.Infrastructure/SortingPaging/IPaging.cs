using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreSample.Infrastructure.SortingPaging
{
    public interface IPaging
    {
        void GetPaginatedResult(int currentPage, int pageSize);
        void GetSortedResult(string sortBy);
    }
}
