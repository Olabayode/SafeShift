using System.ComponentModel.DataAnnotations;

namespace SafeShift.Models.DTOs.Validation;

public class NotDefaultDateTimeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is DateTime dateTime && dateTime != default;
    }
}
