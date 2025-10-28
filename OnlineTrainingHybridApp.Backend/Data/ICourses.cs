using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public interface ICourses
    {
        Task<IEnumerable<Courses>> GetCourses(); // ambil semua
        Task<IEnumerable<Courses>> SearchCourses(string search);
        Task<Courses?> GetCourseById(int id);
        Task<Courses> AddCourse(Courses course);
        Task<Courses?> UpdateCourse(int id, Courses course);
        Task<bool> DeleteCourse(int id);
    }
}
