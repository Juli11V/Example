namespace WebAPI.Domain.Exceptions;
public class CurrencyConversionException : Exception
{
    public CurrencyConversionException(string message) : base(message) { }
}

