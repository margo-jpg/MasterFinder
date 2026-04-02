using FluentValidation;
using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public class ResponsesValidator : AbstractValidator<Response>
    {
        public ResponsesValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("ID заказа должен быть положительным");

            RuleFor(x => x.ExecutorId)
                .GreaterThan(0).WithMessage("ID исполнителя должен быть положительным");

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage("Комментарий не может быть длиннее 1000 символов");
        }
    }
}