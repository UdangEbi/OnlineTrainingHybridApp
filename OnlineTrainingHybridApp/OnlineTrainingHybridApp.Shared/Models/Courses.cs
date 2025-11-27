using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrainingHybridApp.Shared.Models
{
    public class Courses
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Level { get; set; } = "Beginner";
        public string Description { get; set; }
        public int TrainerId { get; set; }
        public Trainers? Trainer { get; set; }
        public string? CoverImage { get; set; }
    }
}
