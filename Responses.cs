using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.Events;
using MasterFinder.Domain.Exceptions;
using MasterFinder.Domain.Validators;

namespace MasterFinder.Domain.Entities
{
    public class Responses : Entity
    {
        public int OrderId { get; private set; }
        public int ExecutorId { get; private set; }
        public string Comment { get; private set; }
        public ResponsesStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Order Order { get; private set; }
        public Executor Executor { get; private set; }

        private Responses() { }

        public Responses(int orderId, int executorId, string comment = null)
        {
            OrderId = orderId;
            ExecutorId = executorId;
            Comment = comment;
            Status = ResponsesStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            var validator = new ResponsesValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
                throw new DomainValidationException(result.Errors);
        }

        public void Withdraw()
        {
            if (Status != ResponsesStatus.Pending)
                throw new DomainException("Можно отозвать только ожидающий отклик",
                    DomainErrorCode.BusinessRuleViolation);

            Status = ResponsesStatus.Withdrawn;
            AddDomainEvent(new ResponsesWithdrawnEvent(Id));
        }

        public void Accept()
        {
            if (Status != ResponsesStatus.Pending)
                throw new DomainException("Можно принять только ожидающий отклик",
                    DomainErrorCode.BusinessRuleViolation);

            Status = ResponsesStatus.Accepted;
        }

        public void Reject()
        {
            if (Status != ResponsesStatus.Pending)
                throw new DomainException("Можно отклонить только ожидающий отклик",
                    DomainErrorCode.BusinessRuleViolation);

            Status = ResponsesStatus.Rejected;
        }
    }
}

