using OnlineShop.Models.Dtos;
using ShopOnline.API.Entities;

namespace ShopOnline.API.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<ProductDto> products, IEnumerable<ProductCategory> productCategories)
        {
            if (products == null || productCategories == null)
            {
                // Handle the case where either products or categories is null
                return Enumerable.Empty<ProductDto>();
            }

            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                        CategoryName = product.CategoryName,
                    }).ToList();
        }
    }
}
