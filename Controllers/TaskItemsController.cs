using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using ToDoApp.ViewModels;


namespace ToDoApp.Controllers
{
    public class TaskItemsController : Controller
    {
        private readonly ApplicationDbContext _context;



        public TaskItemsController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: TaskItems
        public IActionResult Index()
        {
            var tasks = _context.TaskItems
                 .Include(t => t.Category)
                 .Include(t => t.Priority)
                 .ToList();

            var viewModel = tasks.Select(t => new TaskItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Category = t.Category.Name,
                Priority = t.Priority.Level,
                Categories = _context.Categories.ToList(),
                Priorities = _context.Priorities.ToList()
            }).ToList();

            return View(viewModel);
        }

        // GET: TaskItem/Create
        public IActionResult Create()
        {
            var viewModel = new TaskItemViewModel
            {
                Categories = _context.Categories.ToList(),
                Priorities = _context.Priorities.ToList()
            };
            return View(viewModel);
        }

        // POST: TaskItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new TaskItem
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    DueDate = viewModel.DueDate,
                    CategoryId = viewModel.CategoryId,
                    PriorityId = viewModel.PriorityId,

                };

                _context.TaskItems.Add(task);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index)); // Redirect to the index page after creating the task
            }

            // If model is not valid, re-populate the dropdowns and return to the view
            viewModel.Categories = _context.Categories.ToList();
            viewModel.Priorities = _context.Priorities.ToList();
            return View(viewModel);
        }





        // GET: TaskItems/Delete/{id}
        public IActionResult Delete(int id)
        {
            var task = _context.TaskItems
                .FirstOrDefault(t => t.Id == id);  // Find the task by ID

            if (task == null)
            {
                return NotFound();  // If the task is not found, return a 404 error
            }

            return View(task);  // If found, pass the task to the view
        }

        // POST: TaskItems/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.TaskItems
                .FirstOrDefault(t => t.Id == id);  // Find the task by ID

            if (task == null)
            {
                return NotFound();  // If the task is not found, return a 404 error
            }

            _context.TaskItems.Remove(task);  // Remove the task from the database
            _context.SaveChanges();  // Save the changes to the database

            return RedirectToAction(nameof(Index));  // Redirect back to the Task List page
        }





    }
}