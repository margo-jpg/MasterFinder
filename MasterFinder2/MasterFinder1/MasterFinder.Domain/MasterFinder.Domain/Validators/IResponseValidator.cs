using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public interface IResponseValidator
    {
        ValidationResult Validate(Response response);
    }
}