using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.Events;
using MasterFinder.Domain.Exceptions;
using MasterFinder.Domain.Validators;

namespace MasterFinder.Domain.Entities
{
    public class Executors : Entity
    {
        public string Username { get; private set; }
        public string Phone { get; private set; }
        public string Specialization { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Response> _responses = new();
        public IReadOnlyCollection<Response> Responses => _responses.AsReadOnly();

        private readonly List<Execution> _executions = new();
        public IReadOnlyCollection<Execution> Executions => _executions.AsReadOnly();

        private Executors() { }

        public Executors(string username, string phone, string specialization)
        {
            Username = username;
            Phone = phone;
            Specialization = specialization;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            var validator = new ExecutorsValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
                throw new DomainValidationException(result.Errors);
        }

        public Response RespondToOrder(Order order, string comment = null)
        {
            if (order.Status != OrderStatus.Open)
                throw new DomainException("Нельзя откликнуться на закрытый заказ",
                    DomainErrorCode.BusinessRuleViolation);

            if (_responses.Any(r => r.OrderId == order.Id))
                throw new DomainException("Вы уже откликались на этот заказ",
                    DomainErrorCode.BusinessRuleViolation);

            var response = new Response(order.Id, Id, comment);
            _responses.Add(response);

            AddDomainEvent(new ResponseCreatedEvent(Id, order.Id));
            return response;
        }

        public IReadOnlyCollection<Response> GetMyResponses()
        {
            return _responses.AsReadOnly();
        }

        public IReadOnlyCollection<Execution> GetMyExecutions()
        {
            return _executions.AsReadOnly();
        }
    }
}