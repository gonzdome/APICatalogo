using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validation;

public class FirstLetterUpperCaseAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success;

        var firstLetter = value.ToString()[0].ToString();
        if (firstLetter != firstLetter.ToString().ToUpper())
            return new ValidationResult("The first letter must be Uppercase!");

        return ValidationResult.Success;
    }
}
