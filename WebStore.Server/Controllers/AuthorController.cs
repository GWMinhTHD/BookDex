using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;

namespace WebStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public AuthorController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            _unitOfWork = unitOfWork;
            _webHost = webhost;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var authors = await _unitOfWork.Author.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Author author = await _unitOfWork.Author.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Author author, IFormFile? file)
        {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string authorPath = Path.Combine(wwwRootPath, "img\\authorcover");
                    if (!string.IsNullOrEmpty(author.Photo))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, author.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(authorPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    author.Photo = @"\img\authorcover\" + fileName;
                }
                await _unitOfWork.Author.Insert(author);
                await _unitOfWork.Save();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, Author author, IFormFile? file)
        {
            if (id != author.Id)
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
                string authorPath = Path.Combine(wwwRootPath, "img\\authorcover");
                if (!string.IsNullOrEmpty(author.Photo))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, author.Photo.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(authorPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                author.Photo = @"\img\authorcover\" + fileName;
            }
            _unitOfWork.Author.Update(author);
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
            Author author = await _unitOfWork.Author.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(author.Photo))
            {
                string wwwRootPath = _webHost.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, author.Photo.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Author.Delete(author);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}

