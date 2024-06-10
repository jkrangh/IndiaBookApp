using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndiaBookApp.Data;
using IndiaBookApp.Models;
using IndiaBookApp.Data.Repositories;
using Microsoft.CodeAnalysis.Operations;
using IndiaBookApp.Data.Interfaces;

namespace IndiaBookApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBook bookRepository;

        public BooksController(IBook bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Book> books;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = await bookRepository.SearchAsync(searchString);
            }
            else
            {
                books = await bookRepository.GetAllAsync();
            }

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Country,ImageLink,Language,Link,Pages,Title,Year")] Book book)
        {
            if (ModelState.IsValid)
            {
                await bookRepository.AddAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Country,ImageLink,Language,Link,Pages,Title,Year")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  await bookRepository.UpdateAsync(book);
                }
                catch (DbUpdateConcurrencyException) //Behövs denna?
                {
                    if (!await BookExistsAsync(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                await bookRepository.DeleteAsync(book);
            }            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookExistsAsync(int id)
        {
            
            
            return Convert.ToBoolean(await bookRepository.GetByIdAsync(id));
        }
    }
}
