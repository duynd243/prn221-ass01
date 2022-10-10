using BusinessObject;

namespace DataAccess.IRepository;

public interface IProductRepository
{
    Product GetProduct(int id);
    IEnumerable<Product> GetProducts();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    void RestoreProduct(int id);
}