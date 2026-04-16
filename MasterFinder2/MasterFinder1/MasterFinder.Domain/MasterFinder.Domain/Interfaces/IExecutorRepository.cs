using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Interfaces
{
    public interface IExecutorRepository : IRepository<Executor>
    {
        Task<IEnumerable<Executor>> GetBySpecializationAsync(string specialization);
        Task<IEnumerable<Response>> GetExecutorResponsesAsync(int executorId);
        Task<IEnumerable<Execution>> GetExecutorExecutionsAsync(int executorId);
    }
}