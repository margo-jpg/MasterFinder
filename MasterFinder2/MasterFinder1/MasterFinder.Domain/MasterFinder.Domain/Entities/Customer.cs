using MasterFinder.Domain.Base;
using MasterFinder.Domain.ValueObject;

namespace MasterFinder.Domain.Entities
{
    public class Customer : AggregateRoot
    {
        public UserName UserName { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private Customer() { }

        public Customer(string username, string phone)
        {
            UserName = UserName.Create(username);
            Phone = PhoneNumber.Create(phone);
            CreatedAt = DateTime.UtcNow;
        }

        public Order CreateOrder(string title, string? description = null)
        {
            var order = new Order(Id, title, description);
            _orders.Add(order);
            AddDomainEvent(new OrderCreatedDomainEvent(Id, order.Id));
            return order;
        }
    }

    public record OrderCreatedDomainEvent(int CustomerId, int OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}