using System.ComponentModel.DataAnnotations;

namespace OnlineTrainingHybridApp.Backend.Models
{
    public class Trainers
    {
        [Key]
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<Courses>? Courses { get; set; }
    }
}
