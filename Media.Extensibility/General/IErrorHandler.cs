namespace Media.Extensibility.General
{
    public interface IErrorHandler
    {
        void ShowError(int statusCode, string message);
    }
}
