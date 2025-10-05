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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoryById(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategoryById(id);
            var categoryDto = category.ToCategoryDto();
            return Ok(categoryDto);
        }


        // POST NEW CATEGORY
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return StatusCode(404, ModelState);
            }

            var category = createCategoryDto.ToCategory();

            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category.ToCategoryDto());
        }
    }
}
