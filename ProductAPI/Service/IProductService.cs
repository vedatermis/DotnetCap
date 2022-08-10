using ProductAPI.Model;

namespace ProductAPI.Service;

public interface IProductService
{
    List<Product> GetProducts();
    void UpdateCategoryName(UpdateCategoryNameDto updateCategoryNameDto);
}