using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public interface ITrainers
    {
        Task<IEnumerable<Trainers>> GetTrainers();          // ambil semua trainer
        Task<IEnumerable<Trainers>> SearchTrainers(string search);
        Task<Trainers?> GetTrainerById(int id);
        Task<Trainers> AddTrainer(Trainers trainer);
        Task<Trainers?> UpdateTrainer(int id, Trainers trainer);
        Task<bool> DeleteTrainer(int id);
    }
}
