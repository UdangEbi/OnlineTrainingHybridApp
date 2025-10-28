using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public static class SeedData
    {
        public static void Initialize(OnlineTrainingContext db)
        {
            db.Database.EnsureCreated();
            if (db.Trainers.Any())
            {
                db.Trainers.RemoveRange(db.Trainers);
                db.SaveChanges();
            }

            // ===== SEED TRAINERS =====
            var trainers = new Trainers[]
            {
                new Trainers
                {
                    TrainerId = 1,
                    Name = "Budi Santoso",
                    ContactNumber = "081234567890"
                },
                new Trainers
                {
                    TrainerId = 2,
                    Name = "Rina Putri",
                    ContactNumber = "089876543210"
                },
                new Trainers
                {
                    TrainerId = 3,
                    Name = "Andi Saputra",
                    ContactNumber = "082233445566"
                }
            };

            db.Trainers.AddRange(trainers);
            db.SaveChanges();

            // ===== SEED COURSES =====
            var courses = new Courses[]
            {
                new Courses
                {
                    CourseId = 1,
                    Title = "Dasar Pemrograman C#",
                    Duration = 40,
                    Level = "Beginner",
                    Description = "Pelatihan dasar untuk memahami logika dan sintaks C#.",
                    TrainerId = 1
                },
                new Courses
                {
                    CourseId = 2,
                    Title = "Desain UI/UX Modern",
                    Duration = 30,
                    Level = "Intermediate",
                    Description = "Pelajari dasar desain antarmuka dan pengalaman pengguna.",
                    TrainerId = 2
                },
                new Courses
                {
                    CourseId = 3,
                    Title = "Pengembangan Web dengan .NET",
                    Duration = 50,
                    Level = "Advanced",
                    Description = "Bangun aplikasi web menggunakan ASP.NET Core dan Blazor.",
                    TrainerId = 3
                },
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();
        }
    }
}
