using System.ComponentModel.DataAnnotations;

namespace OnlineTrainingHybridApp.Backend.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Level { get; set; } = "Beginner";
        public string Description { get; set; } = string.Empty;
        public int TrainerId { get; set; }
        public Trainers? Trainer { get; set; }
    }
}
