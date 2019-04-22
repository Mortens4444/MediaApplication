using DirectShowLib;
using Media.Extensibility.Filter;
using Media.Extensibility.Filter.FilterCreator;
using Media.Extensibility.Media;
using System.Collections.Generic;

namespace MediaApplication.Media
{
    public class ConnectedFilterProvider : IConnectedFilterProvider
    {
        private readonly ISampleGrabberInitializer sampleGrabberInitializer;
        private readonly IFilterPropertyProvider filterPropertyProvider;

        public ConnectedFilterProvider(ISampleGrabberInitializer sampleGrabberInitializer,
            IFilterPropertyProvider filterPropertyProvider)
        {
            this.sampleGrabberInitializer = sampleGrabberInitializer;
            this.filterPropertyProvider = filterPropertyProvider;
        }

        public IEnumerable<FilterCreatorBase> GetAviPlayerFilters(string filePath)
        {
            return new FilterCreatorBase[]
            {
                new BaseFilterCreator("File Source (Async.)", (IBaseFilter)new AsyncReader(), (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(filePath, null); }),
                new BaseFilterCreator("AVI Splitter", (IBaseFilter)new AviSplitter()),
                new BaseFilterCreator("Mpeg4s Decoder DMO", (IBaseFilter)new DMOWrapperFilter(), (IBaseFilter filter) => { ((IDMOWrapperFilter)filter).Init(filterPropertyProvider.GetGuid(FilterType.Mpeg4sDecoderDmo), filterPropertyProvider.GetGuid(FilterType.Mpeg4sDecoderDmoCat)); }),
                //new BaseFilterCreator("Video Renderer", (IBaseFilter)new VideoRenderer()),
                new TypedFilterCreator(FilterType.VideoRenderer),
                new NamedFilterCreator("Default WaveOut Device")
            };
        }

        public IEnumerable<FilterCreatorBase> GetDeprecatedAviPlayerGraph(string filePath)
        {
            return new FilterCreatorBase[]
            {
                new BaseFilterCreator("AVI/WAV File Source", (IBaseFilter)new AVIDoc(), (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(filePath, null); }),
                new BaseFilterCreator("Mpeg4s Decoder DMO", (IBaseFilter)new DMOWrapperFilter(), (IBaseFilter filter) => { ((IDMOWrapperFilter)filter).Init(filterPropertyProvider.GetGuid(FilterType.Mpeg4sDecoderDmo), filterPropertyProvider.GetGuid(FilterType.Mpeg4sDecoderDmoCat)); }),
                new BaseFilterCreator("Video Renderer", (IBaseFilter)new VideoRenderer())
            };
        }

        public IEnumerable<FilterCreatorBase> GetHpPreviewFilters()
        {
            return new FilterCreatorBase[]
            {
                new NamedFilterCreator("HP Truevision HD"),
                new TypedFilterCreator(FilterType.SampleGrabber, (IBaseFilter filter) => { sampleGrabberInitializer.Initialize(filter as ISampleGrabber); }),
                new BaseFilterCreator("MJPEG Decompressor", (IBaseFilter)new MjpegDec()),
                new BaseFilterCreator("Color Space Converter", (IBaseFilter)new Colour()),
                //new NamedFilterCreator("Glow"),
                new BaseFilterCreator("Video Renderer", (IBaseFilter)new VideoRenderer())
            };
        }

        public IEnumerable<FilterCreatorBase> GetHpPreviewFilters2()
        {
            return new FilterCreatorBase[]
            {
                new NamedFilterCreator("HP Truevision HD"),
                new BaseFilterCreator("Infinite Pin Tee Filter", (IBaseFilter)new InfTee()),
                new BaseFilterCreator("MJPEG Decompressor", (IBaseFilter)new MjpegDec()),
                new BaseFilterCreator("Video Renderer", (IBaseFilter)new VideoRenderer())
            };
        }

        public IEnumerable<FilterCreatorBase> GetHpRecorderFilters(string pathToRecordedFile)
        {
            return new FilterCreatorBase[]
            {
                new NamedFilterCreator("HP Truevision HD"),
                new BaseFilterCreator("Infinite Pin Tee Filter", (IBaseFilter)new InfTee()),
                new BaseFilterCreator("AVI Mux", (IBaseFilter)new AviDest()),
                new BaseFilterCreator("File Writer", (IBaseFilter)new FileWriter(), (IBaseFilter filter) => { ((IFileSinkFilter)filter).SetFileName(pathToRecordedFile, null); })
            };
        }

        public IEnumerable<FilterCreatorBase> GetHpRecorderFilters2(string pathToRecordedFile)
        {
            return new FilterCreatorBase[]
            {
                new NamedFilterCreator("HP Truevision HD"),
                new BaseFilterCreator("Smart Tee", (IBaseFilter)new SmartTee()),
                new BaseFilterCreator("AVI Mux", (IBaseFilter)new AviDest()),
                new BaseFilterCreator("File Writer", (IBaseFilter)new FileWriter(), (IBaseFilter filter) => { ((IFileSinkFilter)filter).SetFileName(pathToRecordedFile, null); })
            };
        }

        public IEnumerable<FilterCreatorBase> GetMicrophoneFilters()
        {            
            return new FilterCreatorBase[]
            {
                //new TypedFilterCreator(FilterType.MicrophoneSource),
                new NamedFilterCreator("Mikrofon (Realtek High Definition Audio)"),                
                new NamedFilterCreator("Default WaveOut Device")
            };
        }

        public IEnumerable<FilterCreatorBase> GetRtspFilters(string url)
        {
            return new FilterCreatorBase[]
            {
                new TypedFilterCreator(FilterType.RtspSource, (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(url, null); }),
                new TypedFilterCreator(FilterType.FFDShowVideoDecoder),
                new TypedFilterCreator(FilterType.VideoRenderer),
            };
        }

        public IEnumerable<FilterCreatorBase> GetSunellRtspFilters(string url)
        {
            return new FilterCreatorBase[]
            {
                new TypedFilterCreator(FilterType.RtspSource, (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(url, null); }),
                new TypedFilterCreator(FilterType.FFDShowVideoDecoder),
                new TypedFilterCreator(FilterType.VideoRenderer3),
            };
        }

        public IEnumerable<FilterCreatorBase> GetDatasteadRtspRtmpHttpOnvifSourceEvaluationFiltersI(string url)
        {
            return new FilterCreatorBase[]
            {
                new TypedFilterCreator(FilterType.DatasteadRtspRtmpHttpOnvifSourceEvaluation, (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(url, null); }),
                new BaseFilterCreator("Video Renderer", (IBaseFilter)new VideoRenderer())
            };
        }

        public IEnumerable<FilterCreatorBase> GetDatasteadRtspRtmpHttpOnvifSourceEvaluationFiltersII(string url)
        {
            return new FilterCreatorBase[]
            {
                new TypedFilterCreator(FilterType.DatasteadRtspRtmpHttpOnvifSourceEvaluation, (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(url, null); }),
                new TypedFilterCreator(FilterType.ColorSpaceConverter),
                new TypedFilterCreator(FilterType.VideoRenderer),
            };
        }

        public IEnumerable<FilterCreatorBase> GetMpegPlayerFilters(string filePath)
        {
            return new FilterCreatorBase[]
            {
                new BaseFilterCreator("File Source (Async.)", (IBaseFilter)new AsyncReader(), (IBaseFilter filter) => { ((IFileSourceFilter)filter).Load(filePath, null); }),
                new TypedFilterCreator(FilterType.Mpeg2Demultiplexer),
                new TypedFilterCreator(FilterType.FFDShowVideoDecoder),
                new TypedFilterCreator(FilterType.VideoRenderer),
                new TypedFilterCreator(FilterType.FFDShowAudioDecoder),
                new NamedFilterCreator("Default DirectSound Device")
            };
        }

        // ScreenCapture
    }
}
