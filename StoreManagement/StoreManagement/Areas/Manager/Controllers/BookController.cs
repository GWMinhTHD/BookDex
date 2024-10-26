using StoreManagement.Interfaces;
using StoreManagement.Models;
using StoreManagement.ViewModels.BookViewModel;
using StoreManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace StoreManagement.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index(string search, int catId=0)
        {
            ViewBag.catId = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            List<Book> books = _unitOfWork.Book.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                books = _unitOfWork.Book.Search(search);
            }
            if (catId != 0)
            {
                books= books.Where(v => v.BookCategories.Select(c => c.CategoryId).Contains(catId)).ToList();
            }
            return View(books);
        }

        public IActionResult Details(int? id)
        {
            Book book = _unitOfWork.Book.GetById(id);
            return View(book);
        }

        public IActionResult Create()
        {

            CreateUpdateVM bookCUvm = new CreateUpdateVM()
            {
                MyAuthors = _unitOfWork.Author.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Alias,
                    Value = a.Id.ToString()
                }),
                MyCategories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Book = new Book()
            };
            List<int> auIds = new List<int>();
            List<int> catIds = new List<int>();

            return View(bookCUvm);
        }
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [HttpPost]
        public IActionResult Create(CreateUpdateVM bookCUvm, IFormFile image, IFormFile? pdf)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.CopyTo(ms);
                            bookCUvm.Book.Cover = ms.ToArray();
                        }
                    }
                }
                if (pdf != null)
                {
                    if (pdf.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            pdf.CopyTo(ms);
                            bookCUvm.Book.FileLocation = ms.ToArray();
                        }
                    }
                }

                if (bookCUvm.AuIDs != null && bookCUvm.AuIDs.Length > 0)
                {
                    foreach (var author in bookCUvm.AuIDs)
                    {
                        bookCUvm.Book.BookAuthors.Add(new BookAuthor()
                        {

                            AuthorId = author
                        });
                    }
                }
                if (bookCUvm.CatIDs != null && bookCUvm.CatIDs.Length > 0)
                {
                    foreach (var category in bookCUvm.CatIDs)
                    {
                        bookCUvm.Book.BookCategories.Add(new BookCategory()
                        {

                            CategoryId = category
                        });
                    }
                }
                _unitOfWork.Book.Insert(bookCUvm.Book);
                TempData["success"] = "Book created succesfully";
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            CreateUpdateVM bookCUvm = new CreateUpdateVM()
            {
                MyAuthors = _unitOfWork.Author.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Alias,
                    Value = a.Id.ToString()
                }),
                MyCategories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Book = new Book()
            };
            List<int> auIds = new List<int>();
            List<int> catIds = new List<int>();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                bookCUvm.Book = _unitOfWork.Book.GetById(id);
                bookCUvm.Book.BookAuthors.ToList().ForEach(res => auIds.Add(res.AuthorId));
                bookCUvm.Book.BookCategories.ToList().ForEach(res => catIds.Add(res.CategoryId));
                bookCUvm.AuIDs = auIds.ToArray();
                bookCUvm.CatIDs = catIds.ToArray();

                return View(bookCUvm);
            }

        }
        [HttpPost]
        public IActionResult Update(CreateUpdateVM bookCUvm, IFormFile? image, IFormFile? pdf)
        {
            if (ModelState.IsValid)
            {
                Book currentBook = _unitOfWork.Book.GetById(bookCUvm.Book.Id);
                currentBook.Name = bookCUvm.Book.Name;
                currentBook.Description = bookCUvm.Book.Description;
                currentBook.Price = bookCUvm.Book.Price;
                currentBook.IsFeatured = bookCUvm.Book.IsFeatured;

                if (image != null)
                {
                    if (image.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.CopyTo(ms);
                            currentBook.Cover = ms.ToArray();
                        }
                    }
                }

                if (pdf != null)
                {
                    if (pdf.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            pdf.CopyTo(ms);
                            currentBook.FileLocation = ms.ToArray();
                        }
                    }
                }

                List<BookAuthor> oldBookAuthors = new List<BookAuthor>();
                bookCUvm.Book.BookAuthors.ToList().ForEach(res => oldBookAuthors.Add(res));
                _unitOfWork.Book.ResetAuthor(bookCUvm.Book);

                List<BookCategory> oldBookCategories = new List<BookCategory>();
                bookCUvm.Book.BookCategories.ToList().ForEach(res => oldBookCategories.Add(res));
                _unitOfWork.Book.ResetCategory(bookCUvm.Book);


                _unitOfWork.Book.Update(currentBook);

                if (bookCUvm.AuIDs != null && bookCUvm.AuIDs.Length > 0)
                {
                    List<BookAuthor> newBookAuthors = new List<BookAuthor>();
                    foreach (var author in bookCUvm.AuIDs)
                    {
                        newBookAuthors.Add(new BookAuthor()
                        {
                            BookId = bookCUvm.Book.Id,
                            AuthorId = author
                        });
                    }
                    _unitOfWork.AddRange(newBookAuthors);
                }

                if (bookCUvm.CatIDs != null && bookCUvm.CatIDs.Length > 0)
                {
                    List<BookCategory> newBookCategories = new List<BookCategory>();
                    foreach (var category in bookCUvm.CatIDs)
                    {
                        newBookCategories.Add(new BookCategory()
                        {
                            CategoryId = category,
                            BookId = bookCUvm.Book.Id
                        });
                    }
                    _unitOfWork.AddRange(newBookCategories);
                }
                TempData["success"] = "Book updated succesfully";
                //_dbContext.SaveChanges();
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(bookCUvm);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Book? book = _dbContext.Book.Find(id);
            Book? book = _unitOfWork.Book.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _unitOfWork.Book.Delete(book);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
