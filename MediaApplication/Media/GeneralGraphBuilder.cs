using DirectShowLib;
using Media.Extensibility.Filter;
using Media.Extensibility.Filter.FilterCreator;
using Media.Extensibility.General;
using Media.Extensibility.Media;
using Media.Extensibility.Media.Connection;
using System.Collections.Generic;
using System.Linq;

namespace MediaApplication.Media
{
    public class GeneralGraphBuilder : IGeneralGraphBuilder
    {
        private readonly IFilterRegister filterRegister;
        private readonly IMediaConnector mediaConnector;
        private readonly IErrorHandler errorHandler;
        private readonly FilterGraph filterGraph;

        public GeneralGraphBuilder(IFilterRegister filterRegister,
            IMediaConnector mediaConnector,
            IErrorHandler errorHandler,
            FilterGraph filterGraph)
        {
            this.filterRegister = filterRegister;
            this.mediaConnector = mediaConnector;
            this.errorHandler = errorHandler;
            this.filterGraph = filterGraph;
        }

        public void BuildGraphWithAutoConnect(IOutConnection outConnection)
        {
            ConnectOutputPins(outConnection);
        }

        private void ConnectOutputPins(IOutConnection outConnection)
        {
            if (outConnection.OutputPins != null)
            {
                foreach (var outputPin in outConnection.OutputPins)
                {
                    if (outputPin.ConnectedPort != null)
                    {
                        mediaConnector.ConnectDirect(outputPin.Filter.Name, outputPin.Name, outputPin.ConnectedPort.Filter.Name, outputPin.ConnectedPort.Name);
                        ConnectOutputPins(outputPin.ConnectedPort.Filter as IOutConnection);
                    }
                }
            }
        }

        public void BuildGraphWithAutoConnect(IEnumerable<FilterCreatorBase> filterCreators)
        {
            RegisterFilters(filterCreators);

            for (int i = 0; i < filterRegister.Filters.Count - 1; i++)
            {
                var filter = filterRegister.Filters.Keys.ElementAt(i);
                var nextFilter = filterRegister.Filters.Keys.ElementAt(i + 1);
                mediaConnector.ConnectDirect(filter, nextFilter);
            }
        }

        public void RegisterFilters(IEnumerable<FilterCreatorBase> filterCreators)
        {
            CreateGraphBuilder();

            foreach (var filterCreator in filterCreators)
            {
                var filter = filterRegister.CreateFilter(filterCreator);
                filterCreator.PostAction?.Invoke(filter);
            }
        }

        public void ManualConnect(string outputFilter, string ouputPinName, string inputFilter, string inputPinName)
        {
            mediaConnector.ConnectDirect(outputFilter, ouputPinName, inputFilter, inputPinName);
        }

        private void CreateGraphBuilder()
        {
            var captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            var statusCode = captureGraphBuilder.SetFiltergraph((IGraphBuilder)filterGraph);
            errorHandler.ShowError(statusCode, "CaptureGraphBuilder SetFiltergraph execution error");
        }
    }
}
