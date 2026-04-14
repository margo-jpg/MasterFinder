using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public interface ICustomerValidator
    {
        ValidationResult Validate(Customer customer);
    }
}