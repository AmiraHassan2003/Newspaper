using System.ComponentModel.DataAnnotations;


public class DateInRangeAttribute : ValidationAttribute
{
    private readonly int _minYear;
    private readonly int _maxYear;

    public DateInRangeAttribute(int minYear, int maxYear)
    {
        _minYear = minYear;
        _maxYear = maxYear;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateTimeValue)
        {
            int year = dateTimeValue.Year;

            if (year < _minYear || year > _maxYear)
            {
                return new ValidationResult(ErrorMessage ?? $"Date must be between {_minYear} and {_maxYear}.");
            }
        }
        return ValidationResult.Success;
    }
}
