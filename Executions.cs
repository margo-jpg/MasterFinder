using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.Exceptions;

namespace MasterFinder.Domain.Entities
{
    public class Executions : Entity
    {
        public int OrderId { get; private set; }
        public int ExecutorId { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        public Order Order { get; private set; }
        public Executor Executor { get; private set; }

        private Executions() { }

        public Executions(int orderId, int executorId)
        {
            OrderId = orderId;
            ExecutorId = executorId;
            StartedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if (CancelledAt != null)
                throw new DomainException("Отмененное выполнение нельзя завершить",
                    DomainErrorCode.BusinessRuleViolation);

            CompletedAt = DateTime.UtcNow;
        }

        public void Cancel(string reason = null)
        {
            if (CompletedAt != null)
                throw new DomainException("Завершенное выполнение нельзя отменить",
                    DomainErrorCode.BusinessRuleViolation);

            CancelledAt = DateTime.UtcNow;
        }
    }
}

