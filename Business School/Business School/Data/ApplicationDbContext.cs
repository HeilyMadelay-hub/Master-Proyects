using Business_School.Models;
using Business_School.Models.JoinTables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Business_School.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        // We include all DbSets, even those of intermediate tables, because they contain extra fields.
        // We only omit DbSets for tables that do not have any additional data.

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<StudentClub> StudentClubs { get; set; } = null!;
        public DbSet<EventClub> EventClubs { get; set; } = null!;
        public DbSet<EventAttendance> EventAttendances { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // === CONFIGURATION OF MAIN KEYS ===
            builder.Entity<StudentClub>()
                .HasKey(sc => new { sc.StudentId, sc.ClubId }); //An student can attend a club

            builder.Entity<EventClub>()
                .HasKey(ec => new { ec.EventId, ec.ClubId }); //An event is related to a club

            builder.Entity<EventAttendance>()
                .HasKey(ea => new { ea.EventId, ea.StudentId }); //Students attend events to gain points

            // === SEEDS

            // 1. Departaments (3)
            builder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Finance & Accounting", PhoneNumber = "601-1001", Email = "finance@businessschool.com", OfficeLocation = "Building A - Room 201" },
                new Department { Id = 2, Name = "Marketing & Sales", PhoneNumber = "601-1002", Email = "marketing@businessschool.com", OfficeLocation = "Building B - Room 105" },
                new Department { Id = 3, Name = "Entrepreneurship", PhoneNumber = "601-1003", Email = "entrepreneur@businessschool.com", OfficeLocation = "Building C - Room 301" }
            );

            // 2. Clubs (4)
            builder.Entity<Club>().HasData(
                new Club { Id = 1, Name = "Finance Club", Description = "Investment and stock market", DepartmentId = 1 },
                new Club { Id = 2, Name = "Marketing Masters", Description = "Digital marketing and branding", DepartmentId = 2 },
                new Club { Id = 3, Name = "Startup League", Description = "Pitch your business idea", DepartmentId = 3 },
                new Club { Id = 4, Name = "Women in Business", Description = "Empowerment and networking", DepartmentId = 2 }
            );

            // 3. Students
            builder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Ana", LastName = "García", Email = "ana@alumnos.com", Level = StudentLevel.Beginner },
                new Student { Id = 2, FirstName = "Carlos", LastName = "López", Email = "carlos@alumnos.com", Level = StudentLevel.Expert },
                new Student { Id = 3, FirstName = "Lucía", LastName = "Martínez", Email = "lucia@alumnos.com", Level = StudentLevel.Advanced },
                new Student { Id = 4, FirstName = "Pablo", LastName = "Ruiz", Email = "pablo@alumnos.com", Level = StudentLevel.Intermediate },
                new Student { Id = 5, FirstName = "Sofía", LastName = "Díaz", Email = "sofia@alumnos.com", Level = StudentLevel.Beginner },
                new Student { Id = 6, FirstName = "Diego", LastName = "Moreno", Email = "diego@alumnos.com", Level = StudentLevel.Intermediate },
                new Student { Id = 7, FirstName = "Elena", LastName = "Jiménez", Email = "elena@alumnos.com", Level = StudentLevel.Beginner },
                new Student { Id = 8, FirstName = "Marcos", LastName = "Vargas", Email = "marcos@alumnos.com", Level = StudentLevel.Expert },
                new Student { Id = 9, FirstName = "Valeria", LastName = "Castillo", Email = "valeria@alumnos.com", Level = StudentLevel.Beginner },
                new Student { Id = 10, FirstName = "Javier", LastName = "Ortega", Email = "javier@alumnos.com", Level = StudentLevel.Intermediate },
                new Student { Id = 11, FirstName = "Clara", LastName = "Romero", Email = "clara@alumnos.com", Level = StudentLevel.Beginner },
                new Student { Id = 12, FirstName = "Hugo", LastName = "Sanz", Email = "hugo@alumnos.com", Level = StudentLevel.Expert }
            );

            // 4. Events
            builder.Entity<Event>().HasData(
    new Event
    {
        Id = 1,
        Title = "Investment Workshop",
        Description = "Learn about stocks",
        StartDate = new DateTime(2025, 12, 10, 18, 0, 0),
        EndDate = new DateTime(2025, 12, 10, 20, 0, 0),
        Capacity = 50,
        PointsReward = 20,
        DepartmentId = 1,
        OrganizerId = null
    },
    new Event { Id = 2, Title = "Digital Marketing Trends 2026", Description = "TikTok and AI", StartDate = new DateTime(2025, 12, 15, 17, 30, 0), EndDate = new DateTime(2025, 12, 15, 19, 0, 0), Capacity = 80, PointsReward = 15, DepartmentId = 2, OrganizerId = null },
    new Event { Id = 3, Title = "Pitch Night", Description = "Present your startup", StartDate = new DateTime(2025, 12, 20, 19, 0, 0), EndDate = new DateTime(2025, 12, 20, 22, 0, 0), Capacity = 40, PointsReward = 30, DepartmentId = 3, OrganizerId = null },
    new Event { Id = 4, Title = "Women Leadership Panel", Description = "Inspiring talks", StartDate = new DateTime(2026, 1, 8, 18, 0, 0), EndDate = new DateTime(2026, 1, 8, 20, 0, 0), Capacity = null, PointsReward = 25, DepartmentId = 2 , OrganizerId = null },
    new Event { Id = 5, Title = "Crypto & Blockchain Basics", Description = "Intro to Web3", StartDate = new DateTime(2026, 1, 15, 17, 0, 0), EndDate = new DateTime(2026, 1, 15, 18, 30, 0), Capacity = 60, PointsReward = 20, DepartmentId = 1, OrganizerId = null },
    new Event { Id = 6, Title = "Startup Weekend", Description = "Build your MVP", StartDate = new DateTime(2026, 1, 25, 9, 0, 0), EndDate = new DateTime(2026, 1, 26, 18, 0, 0), Capacity = 30, PointsReward = 50, DepartmentId = 3, OrganizerId = null }
);

            // 5. Memberships with initial points and leaders
            builder.Entity<StudentClub>().HasData(
                new StudentClub { StudentId = 1, ClubId = 1, JoinedAt = new DateTime(2025, 9, 15), IsLeader = true, PointsFromThisClub = 40 },
                new StudentClub { StudentId = 2, ClubId = 1, JoinedAt = new DateTime(2025, 9, 20), IsLeader = false, PointsFromThisClub = 15 },
                new StudentClub { StudentId = 3, ClubId = 2, JoinedAt = new DateTime(2025, 9, 10), IsLeader = true, PointsFromThisClub = 50 },
                new StudentClub { StudentId = 4, ClubId = 2, JoinedAt = new DateTime(2025, 9, 25), IsLeader = false, PointsFromThisClub = 10 },
                new StudentClub { StudentId = 5, ClubId = 3, JoinedAt = new DateTime(2025, 9, 5), IsLeader = true, PointsFromThisClub = 60 },
                new StudentClub { StudentId = 6, ClubId = 3, JoinedAt = new DateTime(2025, 9, 18), IsLeader = false, PointsFromThisClub = 25 },
                new StudentClub { StudentId = 7, ClubId = 4, JoinedAt = new DateTime(2025, 10, 1), IsLeader = true, PointsFromThisClub = 55 },
                new StudentClub { StudentId = 8, ClubId = 4, JoinedAt = new DateTime(2025, 10, 5), IsLeader = false, PointsFromThisClub = 20 },
                new StudentClub { StudentId = 9, ClubId = 1, JoinedAt = new DateTime(2025, 10, 10), IsLeader = false, PointsFromThisClub = 5 },
                new StudentClub { StudentId = 10, ClubId = 2, JoinedAt = new DateTime(2025, 10, 12), IsLeader = false, PointsFromThisClub = 10 }
            );

            // 6. Event registrations (including some already attended)
            builder.Entity<EventAttendance>().HasData(
                new EventAttendance { EventId = 1, StudentId = 1, RegisteredAt = new DateTime(2025, 12, 1), HasAttended = true, AttendedAt = new DateTime(2025, 12, 10, 18, 30, 0), PointsAwarded = 20 },
                new EventAttendance { EventId = 1, StudentId = 2, RegisteredAt = new DateTime(2025, 12, 2), HasAttended = true, AttendedAt = new DateTime(2025, 12, 10, 18, 15, 0), PointsAwarded = 20 },
                new EventAttendance { EventId = 2, StudentId = 3, RegisteredAt = new DateTime(2025, 12, 5), HasAttended = false, PointsAwarded = 0 },
                new EventAttendance { EventId = 3, StudentId = 5, RegisteredAt = new DateTime(2025, 12, 10), HasAttended = false, PointsAwarded = 0 },
                new EventAttendance { EventId = 4, StudentId = 7, RegisteredAt = new DateTime(2025, 12, 20), HasAttended = false, PointsAwarded = 0 }
            );

            // 7. Relationship Event-Club
            builder.Entity<EventClub>().HasData(
                new EventClub { EventId = 1, ClubId = 1 },
                new EventClub { EventId = 2, ClubId = 2 },
                new EventClub { EventId = 3, ClubId = 3 },
                new EventClub { EventId = 4, ClubId = 4 },
                new EventClub { EventId = 5, ClubId = 1 },
                new EventClub { EventId = 6, ClubId = 3 }
            );
        }
    }
}
