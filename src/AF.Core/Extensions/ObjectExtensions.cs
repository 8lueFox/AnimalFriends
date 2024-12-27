namespace AF.Core.Extensions;

public static class ObjectExtensions
{
    public static void ThrowIfNull(this object value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }
}