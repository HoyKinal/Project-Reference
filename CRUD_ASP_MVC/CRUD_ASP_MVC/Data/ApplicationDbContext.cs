using ASP_MVC_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CRUD_ASP_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationship configurations for M:M
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Initialization by using JSON File
            try
            {
                // Seed data from JSON file for Students
                var studentsJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "students.json"));
                var students = JsonConvert.DeserializeObject<List<Student>>(studentsJson);
                modelBuilder.Entity<Student>().HasData(students?.ToArray() ?? Array.Empty<Student>());

                // Seed data from JSON file for Courses
                var coursesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "courses.json"));
                var courses = JsonConvert.DeserializeObject<List<Course>>(coursesJson);
                modelBuilder.Entity<Course>().HasData(courses?.ToArray() ?? Array.Empty<Course>());

                // Seed data from JSON file for Enrollments
                var enrollmentsJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "enrollments.json"));
                var enrollments = JsonConvert.DeserializeObject<List<Enrollment>>(enrollmentsJson);
                modelBuilder.Entity<Enrollment>().HasData(enrollments?.ToArray() ?? Array.Empty<Enrollment>());
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred while seeding data from JSON files: {ex.Message}");
            }

            // Initialization by object
            // Seed data for Students
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 3, Name = "Alice Johnson" },
                new Student { StudentId = 4, Name = "Bob Williams" }
                // Add more students as needed
            );

            // Seed data for Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 3, CourseName = "Computer Science" },
                new Course { CourseId = 4, CourseName = "Literature" }
                // Add more courses as needed
            );

            // Seed data for Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentId = 3, StudentId = 3, CourseId = 3, EnrollmentDate = DateTime.Now },
                new Enrollment { EnrollmentId = 4, StudentId = 4, CourseId = 4, EnrollmentDate = DateTime.Now }
                // Add more enrollments as needed
            );
        }
    }
}
