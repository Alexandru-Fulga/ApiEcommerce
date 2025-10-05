using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;

namespace ApiEcommerce.Mapping;

public static class MappingConfig
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CreationDate = category.CreationDate
        };
    }

    public static Category ToCategory(this CategoryDto categoryDto)
    {
        return new Category
        {
            Name = categoryDto.Name,
        };
    }

    public static Category ToCategory(this CreateCategoryDto createCategoryDto)
    {
        return new Category
        {
            Name = createCategoryDto.Name
        };
    }

    public static IEnumerable<CategoryDto> ToDtoList(this IEnumerable<Category> categories)
    {
        return categories.Select(c => c.ToCategoryDto());
    }
}
