using MediaApplication.General;
using Media.Extensibility.Filter.FilterCreator;
using Media.Extensibility.Media;
using System;
using System.Collections.Generic;

namespace MediaApplication.Media
{
    public class TypedGraphBuilder : ITypedGraphBuilder
    {
        private readonly IGeneralGraphBuilder generalGraphBuilder;
        private readonly IConnectedFilterProvider connectedFilterProvider;
        private readonly IConnectionProvider connectionProvider;
        private IEnumerable<FilterCreatorBase> registeredFilters;

        public TypedGraphBuilder(IGeneralGraphBuilder generalGraphBuilder,
            IConnectedFilterProvider connectedFilterProvider,
            IConnectionProvider connectionProvider)
        {
            this.generalGraphBuilder = generalGraphBuilder;
            this.connectedFilterProvider = connectedFilterProvider;
            this.connectionProvider = connectionProvider;
        }

        public void Stop()
        {
            foreach (var registeredFilter in registeredFilters)
            {
                registeredFilter.CallMethod("Close");
                registeredFilter.CallMethod("Release");
            }
        }

        public void BuildDeprecatedAviPlayer(string filePath)
        {
            registeredFilters = connectedFilterProvider.GetDeprecatedAviPlayerGraph(filePath);
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }

        public void BuildAviPlayer(string filename)
        {
            registeredFilters = connectedFilterProvider.GetAviPlayerFilters(filename);
            generalGraphBuilder.RegisterFilters(registeredFilters);
            var connections = connectionProvider.GetAviPlayerConnection();
            generalGraphBuilder.BuildGraphWithAutoConnect(connections);
        }

        public void BuildHpCameraPreview()
        {
            registeredFilters = connectedFilterProvider.GetHpPreviewFilters();
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }

        public void BuildHpCameraPreview2()
        {
            registeredFilters = connectedFilterProvider.GetHpPreviewFilters2();
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }
        
        public void BuildMicrophone()
        {
            registeredFilters = connectedFilterProvider.GetMicrophoneFilters();
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }

        public void BuildHpCameraRecorder(string filename)
        {
            registeredFilters = connectedFilterProvider.GetHpRecorderFilters(filename);
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }

        public void BuildHpCameraRecorderWithConnectionProvider(string filename)
        {
            registeredFilters = connectedFilterProvider.GetHpRecorderFilters2(filename);
            generalGraphBuilder.RegisterFilters(registeredFilters);
            var connections = connectionProvider.GetHpRecorderConnection();
            generalGraphBuilder.BuildGraphWithAutoConnect(connections);
        }

        public void BuildRtsp(string url)
        {
            registeredFilters = connectedFilterProvider.GetRtspFilters(url);
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }

        public void BuildSunellRtsp(string url)
        {
            registeredFilters = connectedFilterProvider.GetSunellRtspFilters(url);
            generalGraphBuilder.BuildGraphWithAutoConnect(registeredFilters);
        }

        // TODO: Cannot play sound
        public void BuildMpegPlayer(string filename)
        {
            registeredFilters = connectedFilterProvider.GetMpegPlayerFilters(filename);
            generalGraphBuilder.RegisterFilters(registeredFilters);
            var connections = connectionProvider.GetMpegPlayerConnection();
            generalGraphBuilder.BuildGraphWithAutoConnect(connections);
        }
    }
}
