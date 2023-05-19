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
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // HTTP GET METHOD: CreateOrEdit
        public IActionResult CreateOrEdit(Int32 id = 0)
        {
            if(id == 0)
                return View(new Transaction());
            else
                return View(_context.Transactions.Find(id));
        }

        // HTTP POST METHOD: CreateOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("TransactionId, CategoryId, Amount, Note, Date")] Transaction transaction)
        {
            if(ModelState.IsValid)
            {
                if(transaction.TransactionId == 0)
                    _context.Add(transaction);
                else
                    _context.Update(transaction);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(transaction);
        }

        // HTTP POST METHOD: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Int32 id)
        {
            if(_context.Transactions == null)
                Problem("Набор объектов 'Application.Categories' пуст.");

            var transaction = await _context.Transactions.FindAsync(id);

            if(transaction != null)
                _context.Transactions.Remove(transaction);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}