namespace OnlineTrainingHybridApp.Shared.DTOs
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Level { get; set; } = "Beginner";

        public int TrainerId { get; set; }
        public string? TrainerName { get; set; }
    }
}
