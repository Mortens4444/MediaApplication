using DirectShowLib;
using Media.Extensibility.General;
using Media.Extensibility.Media;
using System.Runtime.InteropServices;

namespace MediaApplication.Media
{
    public class SampleGrabberInitializer : ISampleGrabberInitializer
    {
        private readonly IErrorHandler errorHandler;

        public SampleGrabberInitializer(IErrorHandler errorHandler)
        {
            this.errorHandler = errorHandler;
        }

        public void Initialize(ISampleGrabber sampleGrabber)
        {
            var sampleGrabberFormat = new VideoInfoHeader
            {
                SrcRect = new DsRect(),
                TargetRect = new DsRect(),
                BitRate = 221184000,
                AvgTimePerFrame = 333333,
                BmiHeader = new BitmapInfoHeader
                {
                    Size = 40,
                    Width = 640,
                    Height = 480,
                    Planes = 1,
                    BitCount = 24,
                    Compression = 1196444237,
                    ImageSize = 921600
                }
            };

            var sampleGrabberPmt = new AMMediaType
            {
                majorType = MediaType.Video,
                subType = MediaSubType.MJPG,
                formatType = FormatType.VideoInfo,
                fixedSizeSamples = true,
                formatSize = 88,
                sampleSize = 921600,
                temporalCompression = false,
                formatPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(sampleGrabberFormat))
            };

            Marshal.StructureToPtr(sampleGrabberFormat, sampleGrabberPmt.formatPtr, false);
            var statusCode = sampleGrabber.SetMediaType(sampleGrabberPmt);
            DsUtils.FreeAMMediaType(sampleGrabberPmt);
            errorHandler.ShowError(statusCode, "Can't set media type to sample grabber");
        }
    }
}
