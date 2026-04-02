using FluentValidation;
using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public class OrdersValidator : AbstractValidator<Order>
    {
        public OrdersValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название заказа обязательно")
                .MinimumLength(3).WithMessage("Название должно содержать минимум 3 символа")
                .MaximumLength(200).WithMessage("Название не может быть длиннее 200 символов");

            RuleFor(x => x.Description)
                .MaximumLength(5000).WithMessage("Описание не может быть длиннее 5000 символов");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("ID заказчика должен быть положительным");
        }
    }
}