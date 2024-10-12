using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Models.DTOs;

namespace WebStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            var getBooks = await _unitOfWork.Book.GetAll();
            var books = new List<BookDTO>();
            foreach (var book in getBooks)
            {
                var bookDTO = new BookDTO();
                bookDTO.Id = book.Id;
                bookDTO.Name = book.Name;
                bookDTO.Cover = book.Cover;
                bookDTO.Description = book.Description;
                bookDTO.Price = book.Price;
                bookDTO.IsFeatured = book.IsFeatured;
                //bookDTO.FileLocation = book.FileLocation;
                foreach (var category in book.BookCategories)
                {
                    bookDTO.CategoryIDs.Add(category.Category.Id);
                    bookDTO.Categories.Add(category.Category.Name);
                }
                foreach (var author in book.BookAuthors)
                {
                    bookDTO.AuthorIDs.Add(author.Author.Id);
                    bookDTO.Authors.Add(author.Author.Alias);
                }
                books.Add(bookDTO);
            }
                return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Book book = await _unitOfWork.Book.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDTO = new BookDTO();
            bookDTO.Id = book.Id;
            bookDTO.Name = book.Name;
            bookDTO.Cover = book.Cover;
            bookDTO.Description = book.Description;
            bookDTO.Price = book.Price;
            bookDTO.FileLocation = book.FileLocation;
            foreach (var category in book.BookCategories)
            {
                bookDTO.CategoryIDs.Add(category.Category.Id);
                bookDTO.Categories.Add(category.Category.Name);
            }
            foreach (var author in book.BookAuthors)
            {
                bookDTO.AuthorIDs.Add(author.Author.Id);
                bookDTO.Authors.Add(author.Author.Alias);
            }
            return Ok(bookDTO);
        }

    }
}

