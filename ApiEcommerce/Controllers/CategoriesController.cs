using ApiEcommerce.Mapping;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        // Constructor

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET ALL CATEGORIES
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDto = categories.ToDtoList();
            return Ok(categoriesDto);
        }

        // GET CATEGORY BY ID
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound($"La categoría con id {id} no existe");
            }

            var categoryDto = category.ToCategoryDto();
            return Ok(categoryDto);
        }


        // POST NEW CATEGORY
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.CategoryExists(createCategoryDto.Name))
            {
                ModelState.AddModelError("", "La categoría ya existe");
                return StatusCode(409, ModelState);
            }

            var category = createCategoryDto.ToCategory();

            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category.ToCategoryDto());
        }

        // UPDATE CATEGORY BY ID
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] CreateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoría con id {id} no existe");
            }

            var category = _categoryRepository.GetCategoryById(id);

            if (!_categoryRepository.UpdateCategory(category!))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {category!.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE CATEGORY BY ID
        [HttpDelete("{id:int}", Name = "DeleteCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoría con id {id} no existe");
            }

            var category = _categoryRepository.GetCategoryById(id);

            if (!_categoryRepository.DeleteCategory(category!))
            {
                ModelState.AddModelError("", $"Algo salió mal eliminando el registro {category!.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
