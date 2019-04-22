using DirectShowLib;
using Media.Extensibility.Filter;
using Media.Extensibility.General;
using Media.Extensibility.Media;
using System;

namespace MediaApplication.Media
{
    public class MediaConnector : IMediaConnector
    {
        private readonly FilterGraph filterGraph;
        private readonly IFilterRegister filterRegister;
        private readonly IErrorHandler errorHandler;
        private readonly IPinProvider pinProvider;

        public MediaConnector(FilterGraph filterGraph,
            IFilterRegister filterRegister,
            IErrorHandler errorHandler,
            IPinProvider pinProvider)
        {
            this.filterGraph = filterGraph;
            this.filterRegister = filterRegister;
            this.errorHandler = errorHandler;
            this.pinProvider = pinProvider;
        }

        public void ConnectDirect(string upStreamFilterName, string downStreamFilterName)
        {
            ConnectDirect(upStreamFilterName, downStreamFilterName,
                (filter) => { return pinProvider.GetNextPin(filter, PinDirection.Output); },
                (filter) => { return pinProvider.GetNextPin(filter, PinDirection.Input); });
        }

        public void ConnectDirect(string upStreamFilterName, string upStreamOutputPinName, string downStreamFilterName, string downStreamInputPinName)
        {
            ConnectDirect(upStreamFilterName, downStreamFilterName,
                (filter) => { return pinProvider.GetPin(filter, upStreamOutputPinName); },
                (filter) => { return pinProvider.GetPin(filter, downStreamInputPinName); });
        }

        public void ConnectDirect(string upStreamFilterName, string downStreamFilterName, Func<IBaseFilter, IPin> OutputPinProvider, Func<IBaseFilter, IPin> InputPinProvider)
        {
            var upStreamFilter = filterRegister[upStreamFilterName];
            var downStreamFilter = filterRegister[downStreamFilterName];
            var upStreamOutputPin = OutputPinProvider(upStreamFilter);
            var downStreamInputPin = InputPinProvider(downStreamFilter);
            var hr = ConnectDirect(upStreamOutputPin, downStreamInputPin);
            errorHandler.ShowError(hr, $"Can't connect {upStreamFilterName} and {downStreamFilterName}");
        }

        private int ConnectDirect(IPin upStreamOutputPin, IPin downStreamInputPin, AMMediaType mediaType = null)
        {
            return ((IGraphBuilder)filterGraph).ConnectDirect(upStreamOutputPin, downStreamInputPin, mediaType);
        }
    }
}
