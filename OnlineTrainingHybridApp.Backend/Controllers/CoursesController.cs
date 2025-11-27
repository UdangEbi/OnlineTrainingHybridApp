using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTrainingHybridApp.Backend.Data;
using OnlineTrainingHybridApp.Backend.DTOs;
using OnlineTrainingHybridApp.Backend.Models;
using OnlineTrainingHybridApp.Shared.DTOs;

namespace OnlineTrainingHybridApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourses _courses;
        private readonly IWebHostEnvironment _env;
        public CoursesController(ICourses courses, IWebHostEnvironment env)
        {
            _courses = courses;
            _env = env;
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
                TrainerName = c.Trainer?.Name,
                CoverImage = c.CoverImage
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
                TrainerName = c.Trainer?.Name,
                CoverImage = c.CoverImage
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
                TrainerName = c.Trainer?.Name,
                CoverImage = c.CoverImage
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse(CourseDTO dto)   // ⬅️ TIDAK ADA [FromForm]
        {
            Console.WriteLine($"[SERVER] dto.TrainerId = {dto.TrainerId}");

            if (dto.TrainerId == 0)
            {
                return BadRequest("TrainerId tidak boleh 0.");
            }

            var course = new Courses
            {
                Title = dto.Title,
                Description = dto.Description,
                Duration = dto.Duration,
                Level = dto.Level,
                TrainerId = dto.TrainerId,
                CoverImage = dto.CoverImage
            };

            await _courses.AddCourse(course);
            return Ok(new { Message = "Course added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, CourseDTO dto)   // ⬅️ sama, TANPA [FromForm]
        {
            Console.WriteLine($"[SERVER] dto.TrainerId (PUT) = {dto.TrainerId}");

            if (dto.TrainerId == 0)
            {
                return BadRequest("TrainerId tidak boleh 0.");
            }

            var updated = new Courses
            {
                Title = dto.Title,
                Description = dto.Description,
                Duration = dto.Duration,
                Level = dto.Level,
                TrainerId = dto.TrainerId,
                CoverImage = dto.CoverImage
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
