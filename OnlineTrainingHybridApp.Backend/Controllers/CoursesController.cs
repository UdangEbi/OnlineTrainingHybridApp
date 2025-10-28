using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTrainingHybridApp.Backend.Data;
using OnlineTrainingHybridApp.Backend.DTOs;
using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourses _courses;
        public CoursesController(ICourses courses)
        {
            _courses = courses;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var data = await _courses.GetCourses();
            var dtoList = data.Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                Duration = c.Duration,
                Level = c.Level,
                TrainerId = c.TrainerId,
                TrainerName = c.Trainer?.Name
            });

            return Ok(dtoList);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> SearchCourses([FromQuery] string query)
        {
            var data = await _courses.SearchCourses(query);
            var dtoList = data.Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                Duration = c.Duration,
                Level = c.Level,
                TrainerId = c.TrainerId,
                TrainerName = c.Trainer?.Name
            });

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            var c = await _courses.GetCourseById(id);
            if (c == null) return NotFound();

            var dto = new CourseDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                Duration = c.Duration,
                Level = c.Level,
                TrainerId = c.TrainerId,
                TrainerName = c.Trainer?.Name
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse(CourseDTO dto)
        {
            var course = new Courses
            {
                Title = dto.Title,
                Description = dto.Description,
                Duration = dto.Duration,
                Level = dto.Level,
                TrainerId = dto.TrainerId
            };
            await _courses.AddCourse(course);
            return Ok(new { Message = "Course added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, CourseDTO dto)
        {
            var updated = new Courses
            {
                Title = dto.Title,
                Description = dto.Description,
                Duration = dto.Duration,
                Level = dto.Level,
                TrainerId = dto.TrainerId
            };

            var result = await _courses.UpdateCourse(id, updated);
            if (result == null) return NotFound();

            return Ok(new { Message = "Course updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var deleted = await _courses.DeleteCourse(id);
            if (!deleted) return NotFound();

            return Ok(new { Message = "Course deleted successfully" });
        }
    }
}
