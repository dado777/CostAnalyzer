using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CostAnalyzer.Models;

namespace CostAnalyzer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Categories != null? View(await _context.Categories.ToListAsync()) : Problem("Набор объектов 'Application.Categories' пуст.");
        }

        // HTTP GET METHOD: CreateOrEdit
        public IActionResult CreateOrEdit(Int32 id = 0)
        {
            if(id == 0)
                return View(new Category());
            else
                return View(_context.Categories.Find(id));
        }

        // HTTP POST METHOD: CreateOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("CategoryId, Title, Icon, Type")] Category category)
        {
            if(ModelState.IsValid)
            {
                if(category.CategoryId == 0)
                    _context.Add(category);
                else
                    _context.Update(category);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // HTTP POST METHOD: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Int32 id)
        {
            if(_context.Categories == null)
                Problem("Набор объектов 'Application.Categories' пуст.");

            var category = await _context.Categories.FindAsync(id);

            if(category != null)
                _context.Categories.Remove(category);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
