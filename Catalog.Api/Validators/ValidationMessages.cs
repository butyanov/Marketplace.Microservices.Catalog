namespace Products.Api.Validators;

public static class ValidationMessages
{
    public const string EmptyField = "Field cannot be empty";
    public const string TooShortValue = "Value of a field is too short";
    public const string TooLongValue = "Value of a field is too long";
    public const string TooLowValue = "Value of a field is too low";
    public const string TooHighValue = "Value of a field is too high";
    public const string ValueContainsWrongSymbols = "Field value contains imadmissible symbols";
    public const string IncorrectFormat = "Value is not in correct format";
}