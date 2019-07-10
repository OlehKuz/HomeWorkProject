using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreSample.Infrastructure.SortingPaging
{
    public class Paging : IPaging
    {
        private IEnumerable<Project> _data;
       /* public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling(decimal.Divide(_data.Count, pageSize));
        }*/
        
        /*public string SortBy { get; set; }

        public int PageSize { get; set; } = 2;*/
        /*int firstPage = 1; // obviously
        int lastPage = TotalPages;
        int prevPage = currentPage > firstPage ? currentPage - 1 : firstPage;
        int nextPage = currentPage < lastPage ? currentPage + 1 : lastPage;*/


       // public int TotalPages => (int)Math.Ceiling(_data.Count / (float)PageSize);
        public Paging(IEnumerable<Project> data)
        {
            _data = data;
        }
        public void GetPaginatedResult(int searchedPage, int pageSize = 2)
        {
            _data = _data.Skip((searchedPage - 1) * pageSize).Take(pageSize);
        }
        public void GetSortedResult(string sortBy)
        {
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortBy)
            {
                case "updated_desc":
                    _data = _data.OrderByDescending(s => s.LastUpdated);
                    break;
                default:
                    _data = _data.OrderBy(s => s.LastUpdated);
                    break; 
            }  
        }
    }
}
