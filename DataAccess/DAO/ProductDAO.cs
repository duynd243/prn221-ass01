using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public class ProductDAO
{
    private readonly PRN221_Ass01_FStoreContext _context;

    public ProductDAO(PRN221_Ass01_FStoreContext context)
    {
        _context = context;
    }

    public List<Product?> GetProducts()
    {
        return _context.Products
            .Include(p => p!.Category)
            .ToList();
    }

    public Product GetProduct(int id)
    {
        return _context
            .Products.Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);
    }

    public void AddProduct(Product product)
    {
        var dbProduct = _context.Products.FirstOrDefault(p => p.ProductName == product.ProductName);
        if (dbProduct is not null)
        {
            throw new Exception("Product name already exists");
        }
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product is null) return;
        product.Status = false;
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
    }
    
    public void RestoreProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product is null) return;
        product.Status = true;
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
    }
}