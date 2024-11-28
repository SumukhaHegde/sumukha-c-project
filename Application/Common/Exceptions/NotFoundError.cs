namespace Application.Common.Exceptions
{
    public class NotFoundError
    {
        readonly string _message;

        public NotFoundError(string message)
        {
            _message = message;
        }
    }
}
