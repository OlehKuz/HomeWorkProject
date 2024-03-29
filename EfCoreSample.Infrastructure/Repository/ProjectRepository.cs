﻿using EfCoreSample.Doman;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Infrastructure.Abstraction;
using EfCoreSample.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace EfCoreSample.Infrastructure.Repository
{
    public class ProjectRepository : IRepository<Project, long>
    {
        private readonly EfCoreSampleDbContext _context;

        public ProjectRepository(EfCoreSampleDbContext context)
        {
            _context = context;
        }
        public async Task<Project> FindAsync(long key)
        {
            return await _context.Projects.FindAsync(key);
            
        }

        public IQueryable<Project> Get(Expression<Func<Project, bool>> expression)
        {
            return _context.Projects.Where(expression);          
        }

        public async Task<Project> InsertAsync(Project item)
        {
            var added = _context.Projects.Add(item).Entity;
            await _context.SaveChangesAsync();
             return added;
        }

        public async Task<bool> IsExistAsync(long key)
        {
            return await _context.Projects.AnyAsync(p=>p.Id.Equals(key));
        }

        public Project Update(Project item)
        { 
           var entity = _context.Projects.Find(item.Id);
           if (entity != null) _context.Entry(entity).State =
                            EntityState.Modified;
            _context.Entry(entity).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return entity;                 
        }

        public void UpdateRange(IEnumerable<Project> items)
        {
            var itemsKeys = items.Select(i => i.Id);
            var searchedProjects = _context.Projects.Where(p => itemsKeys.Contains(p.Id));
            _context.Projects.UpdateRange(searchedProjects);
            foreach (var project in searchedProjects)
            {
                _context.Entry(project).CurrentValues.SetValues(items.Single(i=>i.Id.Equals(project.Id)));
            }
            _context.SaveChanges();
        }

        public bool Remove(Project item)
        {
            var exists = _context.Projects.Find(item.Id);
            if (exists == null) return false;
            _context.Entry(exists).State = EntityState.Deleted;
            _context.SaveChanges();
            return true;            
        }

        public  bool Remove(long key)
        {
            var project = _context.Projects.Find(key);
            if (project == null) return false;
            _context.Entry(project).State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }

        public List<object> FindRelated(long key)
        {
            List<Employee> employees = (from empProj in _context.EmployeeProjects
                        join proj in _context.Projects
                        on empProj.ProjectId equals proj.Id
                        join emp in _context.Employees
                        on empProj.EmployeeId equals emp.Id
                        where proj.Id == key
                        select emp).ToList();

            List<object> members = new List<object>();
            foreach (var m in employees)
            {
                members.Add(m);
            }
            return members;
        }
    }
}
