using BusinessObject;

namespace DataAccess.IRepository;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    Category? GetCategory(int id);
}