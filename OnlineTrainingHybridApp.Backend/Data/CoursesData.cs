using Microsoft.EntityFrameworkCore;
using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public class CoursesData : ICourses
    {
        private readonly OnlineTrainingContext _context;
        public CoursesData(OnlineTrainingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Courses>> GetCourses()
        {
            return await _context.Courses
                .Include(c => c.Trainer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Courses>> SearchCourses(string search)
        {
            var query = _context.Courses
                .Include(c => c.Trainer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    EF.Functions.Like(c.Title, $"%{search}%") ||
                    EF.Functions.Like(c.Description, $"%{search}%") ||
                    (c.Trainer != null && EF.Functions.Like(c.Trainer.Name, $"%{search}%"))
                );
            }

            return await query.ToListAsync();
        }

        public async Task<Courses?> GetCourseById(int id)
        {
            return await _context.Courses.Include(c => c.Trainer)
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task<Courses> AddCourse(Courses course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Courses?> UpdateCourse(int id, Courses course)
        {
            var existing = await _context.Courses.FindAsync(id);
            if (existing == null)
                return null;

            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.TrainerId = course.TrainerId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
