using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;

namespace ApiEcommerce.Mapping;

public static class ProductsMappingConfig
{
    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImgUrl = product.ImgUrl,
            SKU = product.SKU,
            Stock = product.Stock,
            CreationDate = product.CreationDate,
            UpdateDate = product.UpdateDate,
            CategoryId = product.CategoryId
        };
    }

    public static Product ToProduct(this CreateProductDto createProductDto, Category category)
    {
        return new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            ImgUrl = createProductDto.ImgUrl,
            SKU = createProductDto.SKU,
            Stock = createProductDto.Stock,
            CategoryId = createProductDto.CategoryId,
            Category = category
        };
    }

    public static void UpdateFromDto(this Product product, UpdateProductDto updateProductDto)
    {
        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.ImgUrl = updateProductDto.ImgUrl;
        product.SKU = updateProductDto.SKU;
        product.Stock = updateProductDto.Stock;
        product.CategoryId = updateProductDto.CategoryId;
        product.UpdateDate = DateTime.Now;
    }

    public static IEnumerable<ProductDto> ToDtoList(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToProductDto());
    }
}
