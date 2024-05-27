namespace PersonelApp.WebAPI.Utilities;

public static class ExtensionMehods
{
    public static string ToErrorResult(this string value)
    {
        return ErrorResult.Failure(value);
    }
}