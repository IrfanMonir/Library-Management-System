using Library_Management_System.Models;
using Library_Management_System.Persistance;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowedBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooks
        public async Task<IActionResult> Index()
        {
            var borrowed = await _context.BorrowedBooks
                .Include(b => b.Books)
                .Include(b => b.Members)
                .ToListAsync();
            return View(borrowed);
        }

        // GET: BorrowedBooks/Create
        public IActionResult Create()
        {
            ViewBag.BookList = new SelectList(_context.Books, "BookID", "Title");
            ViewBag.MemberList = new SelectList(_context.Members, "MemberID", "FirstName");
            return View();
        }

        // POST: BorrowedBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowedBooks borrowedBook)
        {
            if (ModelState.IsValid)
            {
                // Check if book is already borrowed and not returned
                var alreadyBorrowed = _context.BorrowedBooks
                    .Any(b => b.BookID == borrowedBook.BookID && b.ReturnDate == null);

                if (alreadyBorrowed)
                {
                    ModelState.AddModelError("", "This book is already borrowed and not yet returned.");
                    ViewBag.BookList = new SelectList(_context.Books, "BookID", "Title");
                    ViewBag.MemberList = new SelectList(_context.Members, "MemberID", "FirstName");
                    return View(borrowedBook);
                }

                borrowedBook.BorrowDate = DateTime.Now;
                _context.Add(borrowedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrowedBook);
        }

        // Mark as Returned
        public async Task<IActionResult> MarkReturned(int id)
        {
            var borrow = await _context.BorrowedBooks.FindAsync(id);
            if (borrow == null) return NotFound();

            borrow.ReturnDate = DateTime.Now;
            _context.Update(borrow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Delete Borrow Record
        public async Task<IActionResult> Delete(int id)
        {
            var borrow = await _context.BorrowedBooks
                .Include(b => b.Books)
                .Include(b => b.Members)
                .FirstOrDefaultAsync(b => b.BorrowID == id);

            if (borrow == null) return NotFound();
            return View(borrow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrow = await _context.BorrowedBooks.FindAsync(id);
            _context.BorrowedBooks.Remove(borrow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
