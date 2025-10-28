using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTrainingHybridApp.Backend.Data;
using OnlineTrainingHybridApp.Backend.DTOs;
using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainers _trainers;
        private readonly OnlineTrainingContext _context;

        public TrainersController(ITrainers trainers, OnlineTrainingContext context)
        {
            _trainers = trainers;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDTO>>> GetTrainers()
        {
            var data = await _trainers.GetTrainers();
            var dtoList = data.Select(t => new TrainerDTO
            {
                TrainerId = t.TrainerId,
                Name = t.Name,
                ContactNumber = t.ContactNumber
             });

            return Ok(dtoList);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TrainerDTO>>> SearchTrainers([FromQuery] string query)
        {
            var data = await _trainers.SearchTrainers(query);
            var dtoList = data.Select(t => new TrainerDTO
            {
                TrainerId = t.TrainerId,
                Name = t.Name,
                ContactNumber = t.ContactNumber
            });

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDTO>> GetTrainer(int id)
        {
            var t = await _trainers.GetTrainerById(id);
            if (t == null)
                return NotFound();

            var dto = new TrainerDTO
            {
                TrainerId = t.TrainerId,
                Name = t.Name,
                ContactNumber = t.ContactNumber,
                TotalCourses = _context.Courses.Count(c => c.TrainerId == t.TrainerId)
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddTrainer(TrainerDTO dto)
        {
            var trainer = new Trainers
            {
                Name = dto.Name,
                ContactNumber = dto.ContactNumber
            };

            await _trainers.AddTrainer(trainer);
            return Ok(new { Message = "Trainer added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrainer(int id, TrainerDTO dto)
        {
            var updated = new Trainers
            {
                Name = dto.Name,
                ContactNumber = dto.ContactNumber
            };

            var result = await _trainers.UpdateTrainer(id, updated);
            if (result == null)
                return NotFound();

            return Ok(new { Message = "Trainer updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrainer(int id)
        {
            var deleted = await _trainers.DeleteTrainer(id);
            if (!deleted)
                return NotFound();

            return Ok(new { Message = "Trainer deleted successfully" });
        }
    }
}
