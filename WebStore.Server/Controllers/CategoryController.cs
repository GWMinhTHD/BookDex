using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Models.DTOs;

namespace WebStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _unitOfWork.Category.GetAll();
            List<CategoryDTO> result = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                var cateDTO = new CategoryDTO();
                cateDTO.Id = category.Id;
                cateDTO.Name = category.Name;
                result.Add(cateDTO);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int? id)
        {
            Category category = await _unitOfWork.Category.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            await _unitOfWork.Category.Insert(category);
            await _unitOfWork.Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            if (_unitOfWork.Category.GetById(id) == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Update(category);
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
            Category category = await _unitOfWork.Category.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(category);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}
