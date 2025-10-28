using Microsoft.EntityFrameworkCore;
using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public class TrainersData : ITrainers
    {

        private readonly OnlineTrainingContext _context;
        public TrainersData(OnlineTrainingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trainers>> GetTrainers()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task<IEnumerable<Trainers>> SearchTrainers(string search)
        {
            var query = _context.Trainers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t =>
                    EF.Functions.Like(t.Name, $"%{search}%") ||
                    EF.Functions.Like(t.ContactNumber, $"%{search}%")
                );
            }

            return await query.ToListAsync();
        }


        public async Task<Trainers?> GetTrainerById(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<Trainers> AddTrainer(Trainers trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainers?> UpdateTrainer(int id, Trainers trainer)
        {
            var existing = await _context.Trainers.FindAsync(id);
            if (existing == null)
                return null;

            existing.Name = trainer.Name;
            existing.ContactNumber = trainer.ContactNumber;
            existing.Courses = trainer.Courses;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
                return false;

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
