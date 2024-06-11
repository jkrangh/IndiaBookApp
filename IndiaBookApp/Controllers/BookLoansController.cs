using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndiaBookApp.Data;
using IndiaBookApp.Models;
using IndiaBookApp.Data.Interfaces;

namespace IndiaBookApp.Controllers
{
    public class BookLoansController : Controller
    {
        private readonly IBookLoan bookLoanRepository;

        public BookLoansController(IBookLoan bookLoanRepository)
        {
            
            this.bookLoanRepository = bookLoanRepository;
        }

        // GET: BookLoans
        public async Task<IActionResult> Index()
        {
            return View(await bookLoanRepository.GetAllAsync());
        }

        // GET: BookLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await bookLoanRepository.GetByIdAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // GET: BookLoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoanDate,LoanExpires")] BookLoan bookLoan)
        {
            if (ModelState.IsValid)
            {
                bookLoanRepository.AddAsync(bookLoan);
                return RedirectToAction(nameof(Index));
            }
            return View(bookLoan);
        }

        // GET: BookLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await bookLoanRepository.GetByIdAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }
            return View(bookLoan);
        }

        // POST: BookLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoanDate,LoanExpires")] BookLoan bookLoan)
        {
            if (id != bookLoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookLoanRepository.UpdateAsync(bookLoan);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookLoanExistsAsync(bookLoan.Id))
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
            return View(bookLoan);
        }

        // GET: BookLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await bookLoanRepository.GetByIdAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // POST: BookLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookLoan = await bookLoanRepository.GetByIdAsync(id);
            if (bookLoan != null)
            {
                await bookLoanRepository.DeleteAsync(bookLoan);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookLoanExistsAsync(int id)
        {
            return Convert.ToBoolean(await bookLoanRepository.GetByIdAsync(id));
        }
    }
}
