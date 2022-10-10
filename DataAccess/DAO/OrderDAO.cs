using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public class OrderDAO
{
    private readonly PRN221_Ass01_FStoreContext _context;

    public OrderDAO(PRN221_Ass01_FStoreContext context)
    {
        _context = context;
    }

    public IEnumerable<Order> GetOrders()
    {
        return _context.Orders
            .Include(o=>o.Member)
            .Include(o => o.OrderDetails)
            .ToList();
    }

    public IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate)
    {
        return _context.Orders
            .Where(o => o.OrderDate >= fromDate
                        && o.OrderDate <= toDate)
            .Include(o=>o.Member)
            .Include(o => o.OrderDetails)
            .ToList();
    }

    public IEnumerable<Order> GetMemberOrders(int memberId)
    {
        return _context.Orders
            .Where(o => o.MemberId == memberId)
            .Include(o=>o.Member)
            .Include(o => o.OrderDetails)
            .ToList();
    }

    public IEnumerable<Order> GetMemberOrders(int memberId, DateTime fromDate, DateTime toDate)
    {
        return _context.Orders
            .Where(o => o.MemberId == memberId
                        && o.OrderDate >= fromDate
                        && o.OrderDate <= toDate)
            .Include(o=>o.Member)
            .Include(o => o.OrderDetails)
            .ToList();
    }
}