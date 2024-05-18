namespace Middle.WebAPI.Exceptions;

public class WeCannotFindYourUserException : Exception
{
    public WeCannotFindYourUserException()
        : base("We cannot find your user")
    {

    }
}
