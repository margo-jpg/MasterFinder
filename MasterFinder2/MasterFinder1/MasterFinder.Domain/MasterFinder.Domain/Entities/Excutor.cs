using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.ValueObject;
using MasterFinder.Domain.Exceptions;

namespace MasterFinder.Domain.Entities
{
    public class Executor : AggregateRoot
    {
        public UserName UserName { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public Specialization Specialization { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Response> _responses = new();
        public IReadOnlyCollection<Response> Responses => _responses.AsReadOnly();

        private readonly List<Execution> _executions = new();
        public IReadOnlyCollection<Execution> Executions => _executions.AsReadOnly();

        private Executor() { }

        public Executor(string username, string phone, string specialization)
        {
            UserName = UserName.Create(username);
            Phone = PhoneNumber.Create(phone);
            Specialization = Specialization.Create(specialization);
            CreatedAt = DateTime.UtcNow;
        }

        public Response RespondToOrder(Order order, string? comment = null)
        {
            if (order.Status != OrderStatus.Open)
                throw new BusinessRuleViolationException("Нельзя откликнуться на закрытый заказ");

            if (_responses.Any(r => r.OrderId == order.Id))
                throw new BusinessRuleViolationException("Вы уже откликались на этот заказ");

            var response = new Response(order.Id, Id, comment);
            _responses.Add(response);
            AddDomainEvent(new ResponseCreatedDomainEvent(Id, order.Id));
            return response;
        }
    }

    public record ResponseCreatedDomainEvent(int ExecutorId, int OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}