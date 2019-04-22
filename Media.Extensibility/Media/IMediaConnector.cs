namespace Media.Extensibility.Media
{
    public interface IMediaConnector
    {
        void ConnectDirect(string leftFilterName, string rightFilterName);

        void ConnectDirect(string leftFilterName, string leftPinName, string rightFilterName, string rightPinName);
    }
}
