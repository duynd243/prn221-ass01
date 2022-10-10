using BusinessObject;

namespace DataAccess.IRepository;

public interface IOrderRepository
{
    IEnumerable<Order> GetOrders();
    IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate);
    IEnumerable<Order> GetMemberOrders(int memberId);
    IEnumerable<Order> GetMemberOrders(int memberId, DateTime fromDate, DateTime toDate);
}