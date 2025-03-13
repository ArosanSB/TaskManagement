using System;

namespace Application.Guards;

public class Guard
{
    public static void ThrowIfArgumentNull(object? argument, string argumentName)
    {
        if (argument == null)
            throw new ArgumentNullException(argumentName);
    }

    public static void ThrowIfStringIsNullOrEmpty(string? argument, string argumentName)
    {
        if (string.IsNullOrEmpty(argument))
            throw new ArgumentNullException(argumentName);
    }

}
