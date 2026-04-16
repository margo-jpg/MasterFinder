using MasterFinder.Domain.ValueObject.Base;
using MasterFinder.Domain.ValueObject.Exceptions;

namespace MasterFinder.Domain.ValueObject
{
    public class UserName : Base.ValueObject
    {
        public string Value { get; private set; }

        private UserName(string value)
        {
            Value = value;
        }

        public static UserName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("Имя пользователя не может быть пустым");

            if (value.Length < 2)
                throw new ValueObjectValidationException("Имя пользователя должно содержать минимум 2 символа");

            if (value.Length > 100)
                throw new ValueObjectValidationException("Имя пользователя не может быть длиннее 100 символов");

            return new UserName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




