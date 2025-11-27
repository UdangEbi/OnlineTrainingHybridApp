using Microsoft.EntityFrameworkCore;
using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public static class SeedData
    {
        public static void Initialize(OnlineTrainingContext db)
        {
            // Pakai migrate supaya schema DB sesuai dengan model & migrations
            db.Database.Migrate();

            // 🔴 Penting: kalau sudah ada data, JANGAN seed lagi
            if (db.Trainers.Any() || db.Courses.Any())
            {
                return;
            }

            // ===== SEED TRAINERS =====
            var trainers = new List<Trainers>
            {
                new Trainers
                {
                    Name = "Budi Santoso",
                    ContactNumber = "081234567890"
                },
                new Trainers
                {
                    Name = "Rina Putri",
                    ContactNumber = "089876543210"
                },
                new Trainers
                {
                    Name = "Andi Saputra",
                    ContactNumber = "082233445566"
                }
            };

            db.Trainers.AddRange(trainers);
            db.SaveChanges();

            var budi = trainers[0];
            var rina = trainers[1];
            var andi = trainers[2];

            // ===== SEED COURSES =====
            var courses = new Courses[]
            {
                new Courses
                {
                    Title = "Dasar Pemrograman C#",
                    Duration = 40,
                    Level = "Beginner",
                    Description = "Pelatihan dasar untuk memahami logika dan sintaks C#.",
                    TrainerId = budi.TrainerId
                },
                new Courses
                {
                    Title = "Desain UI/UX Modern",
                    Duration = 30,
                    Level = "Intermediate",
                    Description = "Pelajari dasar desain antarmuka dan pengalaman pengguna.",
                    TrainerId = rina.TrainerId
                },
                new Courses
                {
                    Title = "Pengembangan Web dengan .NET",
                    Duration = 50,
                    Level = "Advanced",
                    Description = "Bangun aplikasi web menggunakan ASP.NET Core dan Blazor.",
                    TrainerId = andi.TrainerId
                },
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();
        }
    }
}
