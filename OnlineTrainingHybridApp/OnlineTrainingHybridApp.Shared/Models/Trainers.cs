using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrainingHybridApp.Shared.Models
{
    public class Trainers
    {
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<Courses>? Courses { get; set; }
    }
}
