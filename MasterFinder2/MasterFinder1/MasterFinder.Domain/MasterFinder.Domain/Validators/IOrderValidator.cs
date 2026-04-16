using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public interface IOrderValidator
    {
        ValidationResult Validate(Order order);
    }
}