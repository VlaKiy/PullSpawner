using System;

public static class Exceptor
{
    /// <summary>
    /// Throw exception if verifiable null.
    /// </summary>
    /// <param name="verifiable"></param>
    /// <param name="exception"></param>
    public static void ThrowIfNull(object verifiable, Exception exception)
    {
        if (verifiable.IsNull())
            ThrowException(exception);
    }

    /// <summary>
    /// Throw exception if result of condition false.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="exception"></param>
    public static void ThrowIfFalse(bool condition, Exception exception)
    {
        if (!condition)
            ThrowException(exception);
    }

    /// <summary>
    /// Throw exception if result of condition true.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="exception"></param>
    public static void ThrowIfTrue(bool condition, Exception exception)
    {
        if (condition)
            ThrowException(exception);
    }

    /// <summary>
    /// Object is a null?
    /// </summary>
    /// <param name="verifiable"></param>
    /// <returns>bool</returns>
    /// <exception cref="ArgumentException"></exception>
    public static bool IsNull(this object verifiable)
    {
        if (verifiable.IsNumericType())
            throw new ArgumentException("Can't check on null numeric type. Please don't use numeric type.", "verifiable");

        return verifiable == null;
    }

    /// <summary>
    /// Object is a number?
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>bool</returns>
    public static bool IsNumericType(this object obj)
    {
        if (obj.GetType() == null)
            return false;

        switch (Type.GetTypeCode(obj.GetType()))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }

    private static void ThrowExceptionIfExceptionNull(Exception exception)
    {
        if (exception == null)
            ThrowException(new ArgumentNullException("Exception is null"));
    }

    private static void ThrowException(Exception exception)
    {
        ThrowExceptionIfExceptionNull(exception);

        throw exception;
    }
}