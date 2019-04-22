using DirectShowLib;
using Media.Extensibility.Media;
using Ninject;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MediaApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var kernel = new StandardKernel(new DefaultNinjectModule());

                var graphBuilder = kernel.Get<FilterGraph>();
                var typedGraphBuilder = kernel.Get<ITypedGraphBuilder>();
                
                var url = args.Length != 1 ? "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_175k.mov" : args[0];
                typedGraphBuilder.BuildSunellRtsp(url);

                //typedGraphBuilder.BuildMicrophone();
                //typedGraphBuilder.BuildHpCameraPreview2();
                //typedGraphBuilder.BuildHpCameraRecorder("F:\\test.avi");
                //typedGraphBuilder.BuildMpegPlayer(@"F:\New\Videos\Sony camera\M2U00003.MPG");
                //typedGraphBuilder.BuildDeprecatedAviPlayer(@"F:\New\Downloads\Magyar.Nepmesek.6.DISC.DVDRip.XviD.HUN-moviesite\Magyar.Nepmesek.6.DISC.DVDRip.XviD.HUN-moviesite.avi");

                var mediaEventHandler = kernel.Get<IMediaEventHandler>();
                mediaEventHandler.Start();                

                Console.ReadLine();
                typedGraphBuilder.Stop();

                var mediaControl = (IMediaControl)graphBuilder;
                mediaControl.Stop();
            }
            catch (COMException ex)
            {
                Console.WriteLine($"COM error: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }
    }
}
