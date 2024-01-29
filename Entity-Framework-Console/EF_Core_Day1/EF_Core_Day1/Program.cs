using System;
using System.Collections.Generic;
using System.Linq;
using EF_Core_Day1.Model;
using Microsoft.EntityFrameworkCore;

/*
 Install EF Core NuGet Packages

 1.Microsoft.EntityFrameworkCore
 
   -It contains the fundamental components and APIs for working with databases using Entity Framework Core.
   -It includes base functionality for data access, DbContext, LINQ queries, and entity tracking.

 2.Microsoft.EntityFrameworkCore.Design

   - Provides design-time support
   - It's used mainly for tasks performed during development, such as scaffolding database contexts and migrations.
   - With this package, you can use command-line tools like dotnet ef to execute EF Core commands during development.

 3.Microsoft.EntityFrameworkCore.SqlServer

   - This package is a database provider for Microsoft SQL Server
   - It contains the necessary components to interact with a SQL Server database using Entity Framework Core.

 4.Microsoft.EntityFrameworkCore.Tools

   - Includes additional tools for working with Entity Framework Core.
   - It includes the migrations feature, which allows you to evolve your database schema over time.
   - It also provides tools for generating SQL scripts, managing database migrations, and other tasks related to database schema changes.
   
   - Install Entity Framework Core Tools: dotnet tool install --global dotnet-ef
   - Add Migration: dotnet ef migrations add [MigrationName]
   - Update Database: dotnet ef database update
   - List Migrations: dotnet ef migrations list
   - Script Migration: dotnet ef migrations script
   - Database Drop: dotnet ef database drop

 */

namespace EF_Core_Day1
{
    // Define DbContext class
    public class MyDbContext : DbContext
    {
        //Define connection to database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database provider and connection string
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-23N4L03\\DBSERVER;Initial Catalog=EFC_enrolldb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        }
        
        /*1.Table Students*/

        public DbSet<Student> Students { get; set; } //Define table Students

        /*2.Table Courses*/
        public DbSet<Course> Courses { get; set; } //Define table Courses

        /*3.Table Enrollment*/
        public DbSet<Enrolling> Enrollings { get; set; }    


        //Define primary key and relationship for the table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Student entity primary key
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            
            // Configure the Course entity primary key
            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            
            // Configure the Enrolling entity primary key
            modelBuilder.Entity<Enrolling>().HasKey(e => e.Id);

            //Configure the relationship M:M (1:M, 1:N)
            modelBuilder.Entity<Enrolling>().HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<Enrolling>().HasOne(e => e.Student).WithMany(e => e.Enrollings).HasForeignKey(e => e.StudentId);
            modelBuilder.Entity<Enrolling>().HasOne(e => e.Course).WithMany(e => e.Enrollings).HasForeignKey(e => e.CourseId);
        }
        
    }

    internal class Program
    {
        public void TableEnrolling()
        {
            // Create 

            using (var context = new MyDbContext())
            {
                int courseId = 3;
                int studentId = 2;

                // Check if the combination of CourseId and StudentId already exists
                if (!context.Enrollings.Any(e => e.CourseId == courseId && e.StudentId == studentId))
                {
                    // If it doesn't exist, then proceed with the insertion
                    var newEnrolling = new Enrolling
                    {
                        CourseId = courseId,
                        StudentId = studentId,
                        BeginDate = DateTime.Now,
                        EndDate = new DateTime(2024, 1, 31, 18, 30, 0) // Example: January 31, 2024, 6:30:00 PM
                };

                    context.Enrollings.Add(newEnrolling);
                    context.SaveChanges();
                }
                else
                {
                    // Handle the case where the combination already exists, perhaps by updating the existing record
                    // or notifying the user about the duplicate entry.
                    Console.WriteLine("Is already exist");
                }
            }

            //Read

            using (var context = new MyDbContext())
            {
                var enrollings = context.Enrollings.ToList(); // Do something with the list of students: Retrieving all students from the database
                Console.WriteLine("Number of students: " + enrollings.Count);

                foreach (var er in enrollings)
                {
                    Console.WriteLine(@$"ID:{er.Id}, CourseID:{er.CourseId}, StudentID:{er.StudentId}, BeginDate:{er.BeginDate}, EndDate:{er.EndDate}");
                }

                var specificEnrolling = context.Enrollings.FirstOrDefault(e => e.Id == 1);
                if (specificEnrolling != null)
                {
                    Console.WriteLine("Search by Name!!!");
                    Console.WriteLine(@$"ID:{specificEnrolling.Id}, CourseID:{specificEnrolling.CourseId}, StudentID:{specificEnrolling.StudentId}, BeginDate:{specificEnrolling.BeginDate}, EndDate:{specificEnrolling.EndDate}");
                }
                else
                {
                    Console.WriteLine("Enrolling not found.");
                }

            }

            //Update

            using (var context = new MyDbContext())
            {
                var enrollingToUpdate = context.Enrollings.FirstOrDefault(er => er.Id == 1);
                if (enrollingToUpdate != null)
                {
                    enrollingToUpdate.StudentId = 2;
                    Console.WriteLine("Update enrolling Successfully");
                    context.SaveChanges();
                }
            }

            // Delete
            using (var context = new MyDbContext())
            {
                var enrollingToDelete = context.Enrollings.FirstOrDefault(u => u.Id == 1);
                if (enrollingToDelete != null)
                {
                    context.Enrollings.Remove(enrollingToDelete);
                    Console.WriteLine("Delete item Successfully");
                    context.SaveChanges();
                }
            }

        }

        public void TableCourse()
        {
            // Create 

            using (var context = new MyDbContext())
            {
                try
                {
                    // Enable IDENTITY_INSERT for the 'Courses' table
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Courses ON");

                    //var newCourse = new Course(4,"C Programming","132");  

                    //context.Add(newCourse);

                    //Or

                    //var newCourse = new Course {
                    //    Id = 5,
                    //    Name = "C Programming",
                    //    Code = "321"
                    //};

                    //context.Add(newCourse); 

                    //Or

                    var newCourses = new List<Course>
                    {
                        new Course { Name = "C Programming", Code = "123" },
                        new Course { Name = "Java Programming", Code = "456" },
                        new Course { Name = "Python Programming", Code = "789" },
                        // Add more courses as needed
                    };

                    context.Courses.AddRange(newCourses);

                    context.SaveChanges();
                }
                finally
                {
                    // Disable IDENTITY_INSERT after the insert operation (whether it succeeds or fails)
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Courses OFF");
                }
            }


            //Read

            using (var context = new MyDbContext())
            {
                var courses = context.Courses.ToList(); // Do something with the list of students: Retrieving all students from the database
                Console.WriteLine("Number of students: " + courses.Count);

                foreach (var cs in courses)
                {
                    Console.WriteLine(@$"CourseID: {cs.Id}, CourseName: {cs.Name},CourseCode: {cs.Code}");
                }

                var specificCourse = context.Courses.FirstOrDefault(u => u.Name == "Java Programming");
                if (specificCourse != null)
                {
                    Console.WriteLine("Search by Name!!!");
                    Console.WriteLine(@$"CourseID: {specificCourse.Id}, CourseName: {specificCourse.Name},CourseCode: {specificCourse.Code}");
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            
            }

            //Update

            using (var context = new MyDbContext())
            {
                var courseToUpdate = context.Courses.FirstOrDefault(cs => cs.Id == 1);
                if (courseToUpdate != null)
                {
                    courseToUpdate.Name = "C# Programming";
                    Console.WriteLine("Update Course Successfully");
                    context.SaveChanges();
                }
            }

            // Delete
            //using (var context = new MyDbContext())
            //{
            //    var CourseToDelete = context.Courses.FirstOrDefault(u => u.Id == 2);
            //    if (CourseToDelete != null)
            //    {
            //        context.Courses.Remove(CourseToDelete); 
            //        Console.WriteLine("Delete item Successfully");
            //        context.SaveChanges();
            //    }
            //}

        }
        public void TableStudent()
        {
            // Create

            using (var context = new MyDbContext())
            {
                // Enable IDENTITY_INSERT for the 'Students' table
               // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Students ON");

                var newStudent = new Student
                {
                 //   Id = 1, // Now it's fine since IDENTITY_INSERT is enabled
                    FirstName = "Deth",
                    LastName = "Dean",
                    Gender = "male",
                    Age = 23
                };

                context.Students.Add(newStudent); // Add to DbSet<Student> Students { get; set; }

                context.SaveChanges(); // Save changes to the database

                // Disable IDENTITY_INSERT after the insert operation
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Students OFF");
            }



            // Read
            using (var context = new MyDbContext())
            {
                var students = context.Students.ToList(); // Do something with the list of students: Retrieving all students from the database
                Console.WriteLine("Number of students: " + students.Count);

                foreach (var stu in students)
                {
                    Console.WriteLine(@$"StuID: {stu.Id},
                                      StudFirstname: {stu.FirstName},
                                      StudentLastName: {stu.LastName},
                                      StudentGender: {stu.Gender},
                                      StudentAge: {stu.Age}");
                }

                var specificStudent = context.Students.FirstOrDefault(u => u.FirstName == "hoy");
                if (specificStudent != null)
                {
                    Console.WriteLine($"Found User: {specificStudent.Id}, Last Name: {specificStudent.FirstName}");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }

            // Update
            using (var context = new MyDbContext())
            {
                var studentToUpdate = context.Students.FirstOrDefault(u => u.Id == 1);
                if (studentToUpdate != null)
                {
                    studentToUpdate.LastName = "Smith";
                    context.SaveChanges();
                }
            }

            // Delete
            using (var context = new MyDbContext())
            {
                var studentToDelete = context.Students.FirstOrDefault(u => u.Id == 2);
                if (studentToDelete != null)
                {
                    context.Students.Remove(studentToDelete);
                    context.SaveChanges();
                }
            }
        }
        static void Main(string[] args)
        {
            Program pro = new Program();

            //pro.TableStudent();
            //pro.TableCourse();
            pro.TableEnrolling();
            
        }
    }
}
/*
 The last:
 
 1. Enable Migrations:  add-migration "Name"

 2. Apply Migrations to create the database tables: update-database

 3. Drop database : drop-database -> A option

 4. List Migrations: Get-Migration


 * If you want to remove Migrations ready
 */



/*
Create database EFC_enrolldb
go

use EFC_enrolldb
go

--drop table students
create table students
(
	id int primary key,
	firstname nvarchar(30),
	lastname nvarchar(30),
	gender varchar(15) null,
	age tinyint
) 
go

--drop table courses
Create table courses
(
	id int primary key,
	code varchar(20) unique not null,
	name nvarchar(30) not null
)
go

--drop table enrollings
create table enrollings
(
	id int primary key,
	studentid int,
	courseid int,
	begdate datetime,
	enddate datetime,
	constraint FK_Enrollings_Students foreign key (studentid) references students(id) on update cascade on delete no action,
	constraint FK_Enrollings_Courses foreign key (courseid) references courses(id) on update cascade on delete no action,
)
go
 
 */


/*
 public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public byte Age { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class Enrolling
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public virtual Student Student { get; set; }
    public virtual Course Course { get; set; }
}

----------------------------------------------

using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrolling> Enrollings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your database connection here
        optionsBuilder.UseSqlServer("YourConnectionString");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and other constraints here
        modelBuilder.Entity<Enrolling>()
            .HasOne(e => e.Student)
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrolling>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


Add-Migration [Name]
Update-Database

 
 */