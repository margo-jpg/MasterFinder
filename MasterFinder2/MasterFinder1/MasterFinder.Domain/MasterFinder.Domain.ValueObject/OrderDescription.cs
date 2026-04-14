using MasterFinder.Domain.ValueObject.Base;
using MasterFinder.Domain.ValueObject.Exceptions;

namespace MasterFinder.Domain.ValueObject
{
    public class OrderDescription : Base.ValueObject
    {
        public string? Value { get; }

        private OrderDescription(string? value)
        {
            Value = value;
        }

        public static OrderDescription Create(string? value)
        {
            if (value != null && value.Length > 5000)
                throw new ValueObjectValidationException("Описание заказа не может быть длиннее 5000 символов");

            return new OrderDescription(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value ?? string.Empty;
        }
    }
}




