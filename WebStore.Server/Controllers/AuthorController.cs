using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Models.DTOs;

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
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAll()
        {
            var authors = await _unitOfWork.Author.GetAll();
            List<AuthorDTO> result = new List<AuthorDTO>();
            foreach (var author in authors)
            {
                var authorDTO = new AuthorDTO();
                authorDTO.Id = author.Id;
                authorDTO.Name = author.Alias;
                result.Add(authorDTO);
            }
            return Ok(result);
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
        public async Task<ActionResult> Create(Author author)
        {
                await _unitOfWork.Author.Insert(author);
                await _unitOfWork.Save();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, Author author)
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
            _unitOfWork.Author.Delete(author);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}

