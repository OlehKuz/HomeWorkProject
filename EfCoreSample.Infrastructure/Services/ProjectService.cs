using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Infrastructure.Abstraction;
using AutoMapper;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Communication;
using EfCoreSample.Infrastructure.Services.Communication;
using EfCoreSample.Doman;
using EfCoreSample.Infrastructure.SortingPaging;
using System.Linq;

namespace EfCoreSample.Infrastructure.Services
{
    public class ProjectService : IService<Project,long>
    {
        private readonly IRepository<Project, long> _repo;

       
       // private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IRepository<Project, long> repo)//,  IUnitOfWork unitOfWork)
        {
            _repo = repo;    
            //_unitOfWork = unitOfWork;
        }
        public async Task<bool> AnyAsync(long key)
        {
            return await _repo.IsExistAsync(key);
        }
        public async Task<Response<Project>> InsertAsync(Project entity)
        {
            try
            {
                await _repo.InsertAsync(entity);
                //await _unitOfWork.CompleteAsync();
                return new Response<Project>(entity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Project>($"An error occurred when saving the project: {ex.Message}");
            }
        }
        public async Task<Response<Project>> Update(Project entity)
        {
            try
            {
                _repo.Update(entity);
                //await _unitOfWork.CompleteAsync();
                return new Response<Project>(entity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Project>($"An error occurred when updating the project: {ex.Message}");
            }
        }
        public List<Employee> GetRelated<Employee>(long id)
        {
            var members = _repo.FindRelated(id);
            List<Employee> employees = new List<Employee>();
            foreach (var memb in members)
            {
                if(memb is Employee)
                
                employees.Add((Employee)memb);
            }
            return employees;
        }
        public async Task<Project> FindAsync(long key) 
        {                      
            return await _repo.FindAsync(key);
        }

        public async Task<List<Project>> GetAsync(string sort,
            int? pageNumber, int? pageSize, string status, string title, string startTime, string endTime)
        {
            if (status != null) _repo.Get(s=>s.Status==status.)
            IEnumerable <Project> projects = 
            if(pageSize!=null || pageNumber != null)
            {
                return PaginatedList<Project>.Create(projects, pageNumber ?? 1, pageSize ?? 2);
            }
            return projects.ToList();    
            
            
        }

        public async Task<Response<Project>> UpdateRange(IEnumerable<Project> entities)
        {
            try
            {
                _repo.UpdateRange(entities);
               // await _unitOfWork.CompleteAsync();
                return new Response<Project>(new Project());
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Project>($"An error occurred when updating the project: {ex.Message}");
            }
        }

        public async Task<Response<Project>> DeleteAsync(long key)
        {
            try
            {
                _repo.Remove(key);
               // await _unitOfWork.CompleteAsync();
                return new Response<Project>(new Project());
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Project>($"An error occurred when saving the delete: {ex.Message}");
            }
        }

        public async Task<Response<Project>> DeleteAsync(Project entity)
        {
            try
            {
                _repo.Remove(entity);
               // await _unitOfWork.CompleteAsync();
                return new Response<Project>(entity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new Response<Project>($"An error occurred when saving the delete: {ex.Message}");
            }
        }
    }
}

