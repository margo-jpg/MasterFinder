using MasterFinder.Domain.Base;
using MasterFinder.Domain.Exceptions;

namespace MasterFinder.Domain.Entities
{
    public class Execution : Entity
    {
        public int OrderId { get; private set; }
        public int ExecutorId { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public string? CancelReason { get; private set; }

        public Order? Order { get; private set; }
        public Executor? Executor { get; private set; }

        private Execution() { }

        public Execution(int orderId, int executorId)
        {
            OrderId = orderId;
            ExecutorId = executorId;
            StartedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if (CancelledAt != null)
                throw new BusinessRuleViolationException("Отмененное выполнение нельзя завершить");

            CompletedAt = DateTime.UtcNow;
        }

        public void Cancel(string? reason = null)
        {
            if (CompletedAt != null)
                throw new BusinessRuleViolationException("Завершенное выполнение нельзя отменить");

            CancelledAt = DateTime.UtcNow;
            CancelReason = reason;
        }
    }
}