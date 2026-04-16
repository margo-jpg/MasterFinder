using System.Text.RegularExpressions;
using MasterFinder.Domain.ValueObject.Base;
using MasterFinder.Domain.ValueObject.Exceptions;

namespace MasterFinder.Domain.ValueObject
{
    public class PhoneNumber : Base.ValueObject
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("Номер телефона не может быть пустым");

            var regex = new Regex(@"^\+?[0-9]{10,15}$");
            if (!regex.IsMatch(value))
                throw new ValueObjectValidationException("Неверный формат номера телефона");

            return new PhoneNumber(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




