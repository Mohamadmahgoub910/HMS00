using HMS00.Models;
using Microsoft.EntityFrameworkCore;

namespace HMS00.Data
{
    public class HMSDBContext : DbContext
    {

        // DbSets (Tables)
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        // Constructor accept DbContextOptions
        public HMSDBContext(DbContextOptions<HMSDBContext> options) : base(options)
        {
        }

        // connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=EAGLE\\SQLEXPRESS; Initial Catalog=HMSDB;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust " +
                "Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Doctor entity
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId);

                entity.Property(d => d.Name)
                      .IsRequired();

                entity.HasMany(d => d.Appointments)
                      .WithOne(a => a.Doctor)
                      .HasForeignKey(a => a.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.FullName)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .IsRequired();

                entity.HasMany(u => u.Appointments)
                      .WithOne(a => a.User)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentId);

                entity.Property(a => a.AppointmentDate)
                      .IsRequired();

                entity.Property(a => a.AppointmentTime)
                      .IsRequired();

                // Doctor relation
                entity.HasOne(a => a.Doctor)
                      .WithMany(d => d.Appointments)
                      .HasForeignKey(a => a.DoctorId);

                // User relation
                entity.HasOne(a => a.User)
                      .WithMany(u => u.Appointments)
                      .HasForeignKey(a => a.UserId);

                // Unique constraint: Prevent duplicate appointments for same doctor & time
                entity.HasIndex(a => new { a.DoctorId, a.AppointmentDate, a.AppointmentTime })
                      .IsUnique();
            });
            // 🔹 Doctors Seeding
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, Name = "Dr. Ahmed Ali", Specialization = "Cardiology", ImageUrl = "/images/doctors/doc1.jpg" },
                new Doctor { DoctorId = 2, Name = "Dr. Sara Mahmoud", Specialization = "Dermatology", ImageUrl = "/images/doctors/doc2.jpg" },
                new Doctor { DoctorId = 3, Name = "Dr. Omar Hossam", Specialization = "Radiology", ImageUrl = "/images/doctors/doc3.jpg" },
                new Doctor { DoctorId = 4, Name = "Dr. Mona Adel", Specialization = "Pediatrics", ImageUrl = "/images/doctors/doc4.jpg" },
                new Doctor { DoctorId = 5, Name = "Dr. Hany Nabil", Specialization = "Orthopedics", ImageUrl = "/images/doctors/doc5.jpg" },
                new Doctor { DoctorId = 6, Name = "Dr. Salma Reda", Specialization = "Neurology", ImageUrl = "/images/doctors/doc6.jpg" },
                new Doctor { DoctorId = 7, Name = "Dr. Yasser Tarek", Specialization = "ENT", ImageUrl = "/images/doctors/doc1.jpg" },
                new Doctor { DoctorId = 8, Name = "Dr. Laila Mostafa", Specialization = "Gynecology", ImageUrl = "/images/doctors/doc2.jpg" },
                new Doctor { DoctorId = 9, Name = "Dr. Mohamed Salah", Specialization = "General Surgery", ImageUrl = "/images/doctors/doc3.jpg" },
                new Doctor { DoctorId = 10, Name = "Dr. Eman Hossam", Specialization = "Ophthalmology", ImageUrl = "/images/doctors/doc4.jpg" }
            );

            // 🔹 Users Seeding
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FullName = "Ahmed Hassan", Email = "ahmed.hassan@example.com", Phone = "0101111111" },
                new User { UserId = 2, FullName = "Sara Mohamed", Email = "sara.mohamed@example.com", Phone = "0102222222" },
                new User { UserId = 3, FullName = "Omar Fathy", Email = "omar.fathy@example.com", Phone = "0103333333" },
                new User { UserId = 4, FullName = "Nour Samir", Email = "nour.samir@example.com", Phone = "0104444444" },
                new User { UserId = 5, FullName = "Youssef Adel", Email = "youssef.adel@example.com", Phone = "0105555555" },
                new User { UserId = 6, FullName = "Mona Ali", Email = "mona.ali@example.com", Phone = "0106666666" },
                new User { UserId = 7, FullName = "Karim Tarek", Email = "karim.tarek@example.com", Phone = "0107777777" },
                new User { UserId = 8, FullName = "Heba Salah", Email = "heba.salah@example.com", Phone = "0108888888" },
                new User { UserId = 9, FullName = "Mahmoud Reda", Email = "mahmoud.reda@example.com", Phone = "0109999999" },
                new User { UserId = 10, FullName = "Rana Mostafa", Email = "rana.mostafa@example.com", Phone = "0110000000" },
                new User { UserId = 11, FullName = "Tamer Hany", Email = "tamer.hany@example.com", Phone = "0111111111" },
                new User { UserId = 12, FullName = "Laila Nabil", Email = "laila.nabil@example.com", Phone = "0112222222" },
                new User { UserId = 13, FullName = "Fady Magdy", Email = "fady.magdy@example.com", Phone = "0113333333" },
                new User { UserId = 14, FullName = "Nesma Adel", Email = "nesma.adel@example.com", Phone = "0114444444" },
                new User { UserId = 15, FullName = "Hossam Ezz", Email = "hossam.ezz@example.com", Phone = "0115555555" }
            );
        }
    }
}
