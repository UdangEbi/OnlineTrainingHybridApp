namespace OnlineTrainingHybridApp.Backend.DTOs
{
    public class TrainerDTO
    {
        public int TrainerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        // Menampilkan jumlah course yang diajar
        public int TotalCourses { get; set; }
    }
}
