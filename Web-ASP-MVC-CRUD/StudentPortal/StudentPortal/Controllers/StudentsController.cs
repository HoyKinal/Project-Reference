using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;
namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel) 
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            await _dbContext.Students.AddAsync(student);  
            await _dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> List() 
        { 
            var student = await _dbContext.Students.ToListAsync(); 
            return View(student);   
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(); // Return a 404 Not Found response if the student is not found.
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {  
            var student = await _dbContext.Students.FindAsync(viewModel.Id);

            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await _dbContext.SaveChangesAsync();
            }
             
            return RedirectToAction("List","Students");   
        }

        [HttpPost]
        // GET action for displaying the confirmation page
        public IActionResult Delete(Student viewModel)
        {
            var student = _dbContext.Students.Find(viewModel.Id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        // POST action for handling the actual deletion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Student viewModel)
        {
            var student = await _dbContext.Students.FindAsync(viewModel.Id);

            if (student != null)
            {
                _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

    }
}
