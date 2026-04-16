using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId);
        Task<Customer?> GetByPhoneAsync(string phone);
    }
}