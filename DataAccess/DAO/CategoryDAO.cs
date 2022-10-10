using BusinessObject;

namespace DataAccess.DAO;

public class CategoryDAO
{
    private readonly PRN221_Ass01_FStoreContext _context;
    
    public CategoryDAO(PRN221_Ass01_FStoreContext context)
    {
        _context = context;
    }
    
    public List<Category?> GetCategories()
    {
        return _context.Categories.ToList();
    }
    public Category? GetCategory(int id)
    {
        return _context.Categories.Find(id);
    }
}