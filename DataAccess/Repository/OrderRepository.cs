using BusinessObject;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDAO _orderDao;

    public OrderRepository(OrderDAO orderDao)
    {
        _orderDao = orderDao;
    }

    public IEnumerable<Order> GetOrders()
    {
        return _orderDao.GetOrders();
    }

    public IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate)
    {
        return _orderDao.GetOrders(fromDate, toDate);
    }

    public IEnumerable<Order> GetMemberOrders(int memberId)
    {
        return _orderDao.GetMemberOrders(memberId);
    }

    public IEnumerable<Order> GetMemberOrders(int memberId, DateTime fromDate, DateTime toDate)
    {
        return _orderDao.GetMemberOrders(memberId, fromDate, toDate);
    }
}