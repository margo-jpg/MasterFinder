namespace MasterFinder.Domain.Validators
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public List<string> Errors { get; }

        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<string>();
        }

        public ValidationResult(string error)
        {
            IsValid = false;
            Errors = new List<string> { error };
        }

        public ValidationResult(List<string> errors)
        {
            IsValid = false;
            Errors = errors;
        }

        public static ValidationResult Success() => new ValidationResult();
        public static ValidationResult Fail(string error) => new ValidationResult(error);
        public static ValidationResult Fail(List<string> errors) => new ValidationResult(errors);
    }
}