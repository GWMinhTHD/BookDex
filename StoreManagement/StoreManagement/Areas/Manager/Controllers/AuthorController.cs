using StoreManagement.Interfaces;
using StoreManagement.Models;
using StoreManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StoreManagement.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Author> authors = _unitOfWork.Author.GetAll();
            return View(authors);
        }

        public IActionResult CreateUpdate(int? id)
        {
            Author author = new Author();
            if (id == null || id == 0)
            {
                //Create new Auhtor
                return View(author);
            }
            else
            {
                //Update an Author
                author = _unitOfWork.Author.GetById(id);
                return View(author);
            }

        }
        [HttpPost]
        public IActionResult CreateUpdate(Author author)
        {

            if (ModelState.IsValid)
            {
                if (author.Id == 0)
                {
                    _unitOfWork.Author.Insert(author);
                    TempData["success"] = "Author created succesfully";
                }
                else
                {
                    _unitOfWork.Author.Update(author);
                    TempData["success"] = "Author updated succesfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Author? author = _unitOfWork.Author.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            _unitOfWork.Author.Delete(author);
            _unitOfWork.Save();
            TempData["success"] = "Author deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
