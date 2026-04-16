namespace MasterFinder.Domain.ValueObject.Exceptions
{
    public class ValueObjectValidationException : Exception
    {
        public string ErrorCode { get; } = "VALUE_OBJECT_VALIDATION_ERROR";

        public ValueObjectValidationException(string message) : base(message) { }
    }
}