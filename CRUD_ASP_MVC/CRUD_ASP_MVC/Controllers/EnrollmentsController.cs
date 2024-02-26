using Microsoft.AspNetCore.Mvc;

namespace CRUD_ASP_MVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(object model)
        {
            // Add logic for creating an enrollment
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            // Add logic for retrieving and displaying the edit form
            return View();
        }

        [HttpPost]
        public IActionResult Edit(object model)
        {
            // Add logic for updating an enrollment
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Add logic for deleting an enrollment
            return RedirectToAction("Index");
        }
    }
}
