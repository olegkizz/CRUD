using System;
using System.Collections.Generic;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using IdentityNLayer.Core.Filters;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.DAL.EF.Repositories
{
    public class CoursesRepository : IRepository<Course>, IFilterRepository<Course, CourseFilter>
    {   
        private ApplicationContext _context;

        public CoursesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Course item)
        {
            await _context.Courses.AddAsync(item);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> Filter(CourseFilter filter)
        {
            IQueryable<Course> filteredCourses = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.TitleContains))
                filteredCourses = filteredCourses.Where(c =>
                c.Title.Contains(filter.TitleContains));

            if (!string.IsNullOrWhiteSpace(filter.DescriptionContains))
                filteredCourses = filteredCourses.Where(c =>
                c.Description.Contains(filter.DescriptionContains));

            if (!string.IsNullOrWhiteSpace(filter.TopicTitleContains))
                filteredCourses = filteredCourses.Where(c =>
                c.Topic.Title.Contains(filter.TopicTitleContains));

            if (filter.LessonsFrom.HasValue)
                filteredCourses = filteredCourses.Where(c => c.Lessons.Count() >= filter.LessonsFrom.Value);

            if (filter.LessonsTo.HasValue)
                filteredCourses = filteredCourses.Where(c => c.Lessons.Count() <= filter.LessonsFrom.Value);

            return await filteredCourses
                .Include(c => c.Lessons)
                .Include(c => c.Topic)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> predicate)
        {
            return await _context.Courses
                .Include(c => c.Topic)
                .Include(c => c.Lessons)
                .ThenInclude(l => l.File)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Topic)
                .Include(c => c.Lessons)
                .ThenInclude(l => l.File)
                .ToListAsync();
        }

        public async Task<Course> GetAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .ThenInclude(l => l.File)
                .AsNoTracking()
                .Where(crs => crs.Id == id)
                .SingleAsync();
        }

        public void Update(Course item)
        {
            _context.Courses.Update(item);
        }
    }
}
