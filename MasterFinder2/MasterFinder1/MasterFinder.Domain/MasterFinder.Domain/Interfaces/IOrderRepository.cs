using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOpenOrdersAsync();
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<Response>> GetOrderResponsesAsync(int orderId);
    }
}