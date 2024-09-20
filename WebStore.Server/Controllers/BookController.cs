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
        private readonly IWebHostEnvironment _webHost;

        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            _unitOfWork = unitOfWork;
            _webHost = webhost;
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

        [HttpPost]
        public async Task<ActionResult> Create(Book book, IFormFile? file)
        {
            string wwwRootPath = _webHost.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string bookPath = Path.Combine(wwwRootPath, "img\\bookcover");
                if (!string.IsNullOrEmpty(book.Cover))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, book.Cover.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                book.Cover = @"\img\bookcover\" + fileName;
            }

            await _unitOfWork.Book.Insert(book);
            await _unitOfWork.Save();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, Book book, IFormFile? file)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            if (_unitOfWork.Author.GetById(id) == null)
            {
                return NotFound();
            }
            string wwwRootPath = _webHost.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string bookPath = Path.Combine(wwwRootPath, "img\\bookcover");
                if (!string.IsNullOrEmpty(book.Cover))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, book.Cover.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                book.Cover = @"\img\bookcover\" + fileName;
            }
            _unitOfWork.Book.Update(book);
            await _unitOfWork.Save();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
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
            if (!string.IsNullOrEmpty(book.Cover))
            {
                string wwwRootPath = _webHost.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, book.Cover.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Book.Delete(book);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}

