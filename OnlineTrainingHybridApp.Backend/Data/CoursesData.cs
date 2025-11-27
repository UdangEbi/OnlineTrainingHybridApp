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
            Console.WriteLine($"[REPO] course.TrainerId = {course.TrainerId}");

            // Cek: apakah trainer dengan ID itu benar-benar ada?
            var trainerExists = await _context.Trainers
                .AnyAsync(t => t.TrainerId == course.TrainerId);

            if (!trainerExists)
            {
                throw new Exception($"[REPO] Trainer dengan ID {course.TrainerId} tidak ditemukan di tabel Trainers.");
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Courses?> UpdateCourse(int id, Courses course)
        {
            var existing = await _context.Courses.FindAsync(id);
            if (existing == null)
                return null;

            // update SEMUA field yang boleh diubah
            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.Duration = course.Duration;
            existing.Level = course.Level;
            existing.TrainerId = course.TrainerId;
            existing.CoverImage = course.CoverImage;   // ⬅️ INI YANG PENTING

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
