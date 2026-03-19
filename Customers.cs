using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.Events;
using MasterFinder.Domain.Exceptions;
using MasterFinder.Domain.Validators;

namespace MasterFinder.Domain.Entities
{
    public class Customers : Entity
    {
        public string Username { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private Customers() { }

        public Customers(string username, string phone)
        {
            Username = username;
            Phone = phone;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            var validator = new CustomersValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
                throw new DomainValidationException(result.Errors);
        }

        public Order CreateOrder(string title, string description)
        {
            var order = new Order(Id, title, description);
            _orders.Add(order);

            AddDomainEvent(new OrderCreatedEvent(Id, order.Id));
            return order;
        }

        public IReadOnlyCollection<Order> GetMyOrders()
        {
            return _orders.AsReadOnly();
        }
    }
}




