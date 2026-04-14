using MasterFinder.Domain.ValueObject.Base;
using MasterFinder.Domain.ValueObject.Exceptions;

namespace MasterFinder.Domain.ValueObject
{
    public class Specialization : Base.ValueObject
    {
        public string Value { get; }

        private Specialization(string value)
        {
            Value = value;
        }

        public static Specialization Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("Специализация не может быть пустой");

            if (value.Length > 200)
                throw new ValueObjectValidationException("Специализация не может быть длиннее 200 символов");

            return new Specialization(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




