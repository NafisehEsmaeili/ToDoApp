using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;


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
        public async Task<IActionResult> Index()
        {
            

            // Fetch only the tasks for the logged-in user
            var taskItems = await _context.TaskItems
                
                .Include(t => t.CategoryName)
                .Include(t => t.PriorityLevel)
                .ToListAsync();

            var viewModel = taskItems.Select(t => new TaskItem
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate,
                CategoryName = t.CategoryName,
                PriorityLevel = t.PriorityLevel
            }).ToList();

            return View(viewModel);
        }

        // GET: TaskItems/Create
        public IActionResult Create()
        {
            // Passing categories and priorities to the view for selection in dropdowns
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["PriorityId"] = new SelectList(_context.Priorities, "Id", "Level");

            ViewBag.Categories = _context.Categories.ToList();
            
            return View(new TaskItem());
        }

        // POST: TaskItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,DueDate,CategoryId,PriorityId")] TaskItem taskItem)
        {


            if (ModelState.IsValid)
            {
                if (taskItem != null)
                {
                    _context.Add(taskItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }


                    // If model state is invalid, repopulate dropdowns and return to the view
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", taskItem.CategoryId);
                    ViewData["PriorityId"] = new SelectList(_context.Priorities, "Id", "Level", taskItem.PriorityId);
            // In case of errors, pass the categories to the view again
            ViewBag.Categories = _context.Categories.ToList();
            
            return View(taskItem);
                }
            
        

            // GET: TaskItems/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(m => m.Id == id);

            

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", taskItem.CategoryId);
            ViewData["PriorityId"] = new SelectList(_context.Priorities, "Id", "Level", taskItem.PriorityId);
            return View(taskItem);
        }

        // POST: TaskItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,CategoryId,PriorityId,IsCompleted")] TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If model state is invalid, repopulate dropdowns and return to the view
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", taskItem.CategoryId);
            ViewData["PriorityId"] = new SelectList(_context.Priorities, "Id", "Level", taskItem.PriorityId);
            return View(taskItem);
        }

        // GET: TaskItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                // Handle the case where the category does not exist (e.g., return an error or set a default category)
                ModelState.AddModelError("", "The selected category does not exist.");
                return View(taskItem);  // or handle it accordingly
            }


            return View(taskItem);
        }

        // POST: TaskItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            

            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}