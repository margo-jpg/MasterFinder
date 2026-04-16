using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public interface IExecutorValidator
    {
        ValidationResult Validate(Executor executor);
    }
}