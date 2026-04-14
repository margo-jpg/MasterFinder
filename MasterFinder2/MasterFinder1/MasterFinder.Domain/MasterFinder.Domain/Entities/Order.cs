using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.ValueObject;
using MasterFinder.Domain.Exceptions;

namespace MasterFinder.Domain.Entities
{
    public class Order : AggregateRoot
    {
        public int CustomerId { get; private set; }
        public OrderTitle Title { get; private set; }
        public OrderDescription Description { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Response> _responses = new();
        public IReadOnlyCollection<Response> Responses => _responses.AsReadOnly();

        public Execution? Execution { get; private set; }

        private Order() { }

        public Order(int customerId, string title, string? description = null)
        {
            CustomerId = customerId;
            Title = OrderTitle.Create(title);
            Description = OrderDescription.Create(description);
            Status = OrderStatus.Open;
            CreatedAt = DateTime.UtcNow;
        }

        public void AcceptResponse(int responseId)
        {
            if (Status != OrderStatus.Open)
                throw new BusinessRuleViolationException("Заказ уже не в статусе Open");

            var response = _responses.FirstOrDefault(r => r.Id == responseId);
            if (response == null)
                throw new NotFoundException("Отклик", responseId);

            response.Accept();

            foreach (var otherResponse in _responses.Where(r => r.Id != responseId))
            {
                if (otherResponse.Status == ResponseStatus.Pending)
                    otherResponse.Reject();
            }

            AddDomainEvent(new ResponseAcceptedDomainEvent(responseId, Id));
        }

        public void StartExecution(int executorId)
        {
            if (Status != OrderStatus.Open)
                throw new BusinessRuleViolationException("Заказ должен быть открыт");

            var acceptedResponse = _responses.FirstOrDefault(r =>
                r.ExecutorId == executorId && r.Status == ResponseStatus.Accepted);

            if (acceptedResponse == null)
                throw new BusinessRuleViolationException("Этот исполнитель не был принят на заказ");

            Status = OrderStatus.InProgress;
            Execution = new Execution(Id, executorId);

            AddDomainEvent(new ExecutionStartedDomainEvent(Id, executorId));
        }

        public void Complete()
        {
            if (Status != OrderStatus.InProgress)
                throw new BusinessRuleViolationException("Заказ не в работе");

            if (Execution == null)
                throw new BusinessRuleViolationException("Нет информации о выполнении");

            Status = OrderStatus.Completed;
            Execution.Complete();

            AddDomainEvent(new OrderCompletedDomainEvent(Id));
        }

        public void Cancel(string? reason = null)
        {
            if (Status == OrderStatus.Completed)
                throw new BusinessRuleViolationException("Завершенный заказ нельзя отменить");

            Status = OrderStatus.Cancelled;

            if (Execution != null && Execution.StartedAt != null && Execution.CompletedAt == null)
            {
                Execution.Cancel(reason);
            }

            AddDomainEvent(new OrderCancelledDomainEvent(Id, reason));
        }
    }

    public record ResponseAcceptedDomainEvent(int ResponseId, int OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    public record ExecutionStartedDomainEvent(int OrderId, int ExecutorId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    public record OrderCompletedDomainEvent(int OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    public record OrderCancelledDomainEvent(int OrderId, string? Reason) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}