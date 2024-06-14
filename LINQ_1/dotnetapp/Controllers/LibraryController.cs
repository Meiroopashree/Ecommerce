using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// using dotnetapp.Data; // Import your DbContext namespace
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class LibraryController : Controller
    {
        private readonly AppDbContext _context;

        public LibraryController(AppDbContext context)
        {
            _context = context;
        }

        // Implement a method to display books associated with a library card.
        public IActionResult DisplayBooksForLibraryCard(int libraryCardId)
        {
            Console.WriteLine(libraryCardId);
            var libraryCard = _context.LibraryCards.FirstOrDefault(lc => lc.Id == libraryCardId);

            if (libraryCard == null)
            {
                return NotFound(); // Handle the case where the library card with the given ID doesn't exist
            }

            var books = _context.Books
                .Where(b => b.LibraryCardId == libraryCardId)
                .ToList();

            return View(books);
        }

        public IActionResult AddBook()
        {
            return View(); // Return the view with validation errors if the model is not valid
        }


        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("DisplayAllBooks");
            }
            return View(book); // Return the view with validation errors if the model is not valid
        }


        // Implement a method to display all books in the library.
        public IActionResult DisplayAllBooks()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpGet]
        [HttpPost]
                // Method to search for books by title
        public IActionResult SearchBooksByTitle(string query)
        {
            // If query is null or empty, return all books
            if (string.IsNullOrEmpty(query))
            {
                var allBooks = _context.Books.ToList();
                return View("DisplayAllBooks", allBooks);
            }

            // Otherwise, filter books by title
            var filteredBooks = _context.Books
                .ToList() // Materialize the query
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View("DisplayAllBooks", filteredBooks);
        }

        // Implement a method to get available books.
        public IActionResult GetAvailableBooks()
        {
            var availableBooks = _context.Books
                .Where(b => b.LibraryCardId == null) // Books that are not borrowed
                .ToList();

            return View(availableBooks);
        }

        // Implement a method to get borrowed books.
        // public IActionResult GetBorrowedBooks()
        // {
        //     var borrowedBooks = _context.Books
        //         .Where(b => b.LibraryCardId != null) // Books that are borrowed
        //         .ToList();

        //     return View(borrowedBooks);
        // }

    }
}
