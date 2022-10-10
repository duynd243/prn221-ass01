using BusinessObject;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ProductDAO _productDao;

    public ProductRepository(ProductDAO productDao)
    {
        _productDao = productDao;
    }

    public Product GetProduct(int id)
    {
        return _productDao.GetProduct(id);
    }

    public IEnumerable<Product> GetProducts()
    {
        return _productDao.GetProducts();
    }

    public void AddProduct(Product product)
    {
        _productDao.AddProduct(product);
    }

    public void UpdateProduct(Product product)
    {
        _productDao.UpdateProduct(product);
    }

    public void DeleteProduct(int id)
    {
        _productDao.DeleteProduct(id);
    }

    public void RestoreProduct(int id)
    {
        _productDao.RestoreProduct(id);
    }
}