using DirectShowLib;

namespace Media.Extensibility.Media
{
    public interface IPinProvider
    {
        IPin GetNextPin(IBaseFilter filter, PinDirection pinDirection);

        IPin GetPin(IBaseFilter filter, string pinname);
    }
}
