using Media.Extensibility.Media.Connection;

namespace Media.Extensibility.Media
{
    public interface IConnectionProvider
    {
        IOutConnection GetHpRecorderConnection();

        IOutConnection GetAviPlayerConnection();

        IOutConnection GetMpegPlayerConnection();
    }
}
