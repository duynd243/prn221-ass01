using BusinessObject;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly CategoryDAO _categoryDao;
    public CategoryRepository(CategoryDAO categoryDao)
    {
        _categoryDao = categoryDao;
    }
    public IEnumerable<Category> GetCategories()
    {
        return _categoryDao.GetCategories();
    }

    public Category? GetCategory(int id)
    {
        return _categoryDao.GetCategory(id);
    }
}