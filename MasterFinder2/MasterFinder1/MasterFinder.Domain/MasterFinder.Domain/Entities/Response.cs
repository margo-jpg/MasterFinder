using MasterFinder.Domain.Base;
using MasterFinder.Domain.Enums;
using MasterFinder.Domain.Exceptions;

namespace MasterFinder.Domain.Entities
{
    public class Response : Entity
    {
        public int OrderId { get; private set; }
        public int ExecutorId { get; private set; }
        public string? Comment { get; private set; }
        public ResponseStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Order? Order { get; private set; }
        public Executor? Executor { get; private set; }

        private Response() { }

        public Response(int orderId, int executorId, string? comment = null)
        {
            OrderId = orderId;
            ExecutorId = executorId;
            Comment = comment;
            Status = ResponseStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void Withdraw()
        {
            if (Status != ResponseStatus.Pending)
                throw new BusinessRuleViolationException("Можно отозвать только ожидающий отклик");

            Status = ResponseStatus.Withdrawn;
        }

        public void Accept()
        {
            if (Status != ResponseStatus.Pending)
                throw new BusinessRuleViolationException("Можно принять только ожидающий отклик");

            Status = ResponseStatus.Accepted;
        }

        public void Reject()
        {
            if (Status != ResponseStatus.Pending)
                throw new BusinessRuleViolationException("Можно отклонить только ожидающий отклик");

            Status = ResponseStatus.Rejected;
        }
    }
}