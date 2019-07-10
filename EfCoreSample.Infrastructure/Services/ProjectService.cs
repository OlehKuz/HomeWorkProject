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
using EfCoreSample.Persistance;
using System.Transactions;

namespace EfCoreSample.Infrastructure.Services
{
    public class ProjectService : IService<Project,long>
    {
        private readonly IRepository<Project, long> _repo;
        public ProjectService(IRepository<Project, long> repo)
        {
            _repo = repo;    
        }

        public async Task<Response<Project>> InsertAsync(Project entity)
        {
            try
            {
                using (var dbContext = new DbContextFactory().CreateDbContext(new string[] { }))
                {
                     var inserted = await _repo.InsertAsync(entity);
                     await dbContext.SaveChangesAsync();
                     return new Response<Project>(inserted);
                }
            }
            catch (Exception ex)
            {
                return new Response<Project>($"An error occurred when saving the project: {ex.Message}");
            }
        }


        public async Task<Response<Project>> Update(Project entity)
        {
            try{
                using (var dbContext = new DbContextFactory().CreateDbContext(new string[] { }))
                {
                    var updated = _repo.Update(entity);
                    await dbContext.SaveChangesAsync();
                    return new Response<Project>(updated);
                }
            }
            catch (Exception ex)
            {
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


        public IEnumerable<Project> Get(string status, string title, string startTime, string endTime)
        {
            IQueryable<Project> projects = _repo.Get(r => true);
            return Filter.GetFiltered(projects, status, title, startTime, endTime).AsEnumerable();
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
            if (!_repo.FindAsync(key)
                    .Result.Status.Equals(EnumExtention
                    .GetDescriptionFromEnumValue(EProjectStatus.InProgress)))
            {
                try
                {
                    using (var dbContext = new DbContextFactory().CreateDbContext(new string[] { }))
                    {
                        var success = _repo.Remove(key);
                        await dbContext.SaveChangesAsync();
                        return new Response<Project>(success);
                    }
                }
                catch (Exception ex)
                {
                    return new Response<Project>($"An error occurred when saving the delete: {ex.Message}");
                }
            }
            else return new Response<Project>($"You can't delete an active project! Project not deleted. ");
        }

        public async Task<Response<Project>> DeleteAsync(Project entity)
        {
            return await DeleteAsync(entity.Id);
        }

        public Task<bool> AnyAsync(long key)
        {
            return _repo.IsExistAsync(key);
        }
    }
}

