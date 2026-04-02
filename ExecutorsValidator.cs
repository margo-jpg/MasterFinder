using FluentValidation;
using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Validators
{
    public class ExecutorsValidator : AbstractValidator<Executor>
    {
        public ExecutorsValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Имя пользователя обязательно")
                .MinimumLength(2).WithMessage("Имя должно содержать минимум 2 символа")
                .MaximumLength(100).WithMessage("Имя не может быть длиннее 100 символов");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Телефон обязателен")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Неверный формат телефона");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Специализация обязательна")
                .MaximumLength(200).WithMessage("Специализация не может быть длиннее 200 символов");
        }
    }
}

