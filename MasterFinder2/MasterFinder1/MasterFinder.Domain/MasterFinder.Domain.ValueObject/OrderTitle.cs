using MasterFinder.Domain.ValueObject.Base;
using MasterFinder.Domain.ValueObject.Exceptions;

namespace MasterFinder.Domain.ValueObject
{
    public class OrderTitle : Base.ValueObject
    {
        public string Value { get; }

        private OrderTitle(string value)
        {
            Value = value;
        }

        public static OrderTitle Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("Название заказа не может быть пустым");

            if (value.Length < 3)
                throw new ValueObjectValidationException("Название заказа должно содержать минимум 3 символа");

            if (value.Length > 200)
                throw new ValueObjectValidationException("Название заказа не может быть длиннее 200 символов");

            return new OrderTitle(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




