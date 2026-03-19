using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.Events;
using MasterFinder.Domain.Exceptions;
using MasterFinder.Domain.Validators;

namespace MasterFinder.Domain.Entities
{
    public class Orders : Entity
    {
        public int CustomerId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public OrdersStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Customer Customer { get; private set; }

        private readonly List<Response> _responses = new();
        public IReadOnlyCollection<Response> Responses => _responses.AsReadOnly();

        public Execution Execution { get; private set; }

        private Orders() { }

        public Orders(int customerId, string title, string description)
        {
            CustomerId = customerId;
            Title = title;
            Description = description;
            Status = OrdersStatus.Open;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            var validator = new OrdersValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
                throw new DomainValidationException(result.Errors);
        }

        public IReadOnlyCollection<Response> GetResponses()
        {
            return _responses.AsReadOnly();
        }

        public void AcceptResponse(int responseId)
        {
            if (Status != OrdersStatus.Open)
                throw new DomainException("Заказ уже не в статусе Open",
                    DomainErrorCode.BusinessRuleViolation);

            var response = _responses.FirstOrDefault(r => r.Id == responseId);
            if (response == null)
                throw new DomainException("Отклик не найден", DomainErrorCode.EntityNotFound);

            response.Accept();

            foreach (var otherResponse in _responses.Where(r => r.Id != responseId))
            {
                if (otherResponse.Status == ResponseStatus.Pending)
                    otherResponse.Reject();
            }

            AddDomainEvent(new ResponseAcceptedEvent(responseId, Id));
        }

        public void StartExecution(int executorId)
        {
            if (Status != OrdersStatus.Open)
                throw new DomainException("Заказ должен быть открыт",
                    DomainErrorCode.BusinessRuleViolation);

            var acceptedResponse = _responses.FirstOrDefault(r =>
                r.ExecutorId == executorId && r.Status == ResponseStatus.Accepted);

            if (acceptedResponse == null)
                throw new DomainException("Этот исполнитель не был принят на заказ",
                    DomainErrorCode.BusinessRuleViolation);

            Status = OrdersStatus.InProgress;
            Execution = new Execution(Id, executorId);

            AddDomainEvent(new ExecutionStartedEvent(Id, executorId));
        }

        public void Complete()
        {
            if (Status != OrdersStatus.InProgress)
                throw new DomainException("Заказ не в работе",
                    DomainErrorCode.BusinessRuleViolation);

            if (Execution == null)
                throw new DomainException("Нет информации о выполнении",
                    DomainErrorCode.EntityNotFound);

            Status = OrdersStatus.Completed;
            Execution.Complete();

            AddDomainEvent(new OrdersCompletedEvent(Id));
        }

        public void Cancel(string reason = null)
        {
            if (Status == OrdersStatus.Completed)
                throw new DomainException("Завершенный заказ нельзя отменить",
                    DomainErrorCode.BusinessRuleViolation);

            Status = OrdersStatus.Cancelled;

            if (Execution != null && Execution.StartedAt != null && Execution.CompletedAt == null)
            {
                Execution.Cancel(reason);


            }

            AddDomainEvent(new OrdersCancelledEvent(Id, reason));
        }
    }
}

