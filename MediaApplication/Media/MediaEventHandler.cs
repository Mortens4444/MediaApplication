using DirectShowLib;
using Media.Extensibility.General;
using Media.Extensibility.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaApplication.Media
{
    public class MediaEventHandler : IMediaEventHandler
    {
        private readonly FilterGraph filterGraph;
        private readonly IErrorHandler errorHandler;
        private readonly ITypedGraphBuilder typedGraphBuilder;

        public MediaEventHandler(FilterGraph filterGraph,
            IErrorHandler errorHandler,
            ITypedGraphBuilder typedGraphBuilder)
        {
            this.filterGraph = filterGraph;
            this.errorHandler = errorHandler;
            this.typedGraphBuilder = typedGraphBuilder;
        }

        public void Start()
        {
            var mediaControl = (IMediaControl)filterGraph;
            int statusCode = mediaControl.Run();
            if (statusCode < 0)
            {
                errorHandler.ShowError(statusCode, "Can't run the graph");
            }

            var mediaEvent = (IMediaEvent)filterGraph;
            bool stop = false;
            while (!stop)
            {
                Thread.Sleep(500);
                Application.DoEvents();
                while (mediaEvent.GetEvent(out EventCode ev, out IntPtr p1, out IntPtr p2, 0) == 0)
                {
                    if ((ev == EventCode.Complete) || (ev == EventCode.UserAbort))
                    {
                        typedGraphBuilder.Stop();
                        mediaControl.Stop();
                        stop = true;
                    }
                    else
                    {
                        if (ev == EventCode.ErrorAbort)
                        {
                            Console.WriteLine("An error occured: HRESULT=(0:X}", p1);
                            typedGraphBuilder.Stop();
                            mediaControl.Stop();
                            stop = true;
                        }
                        mediaEvent.FreeEventParams(ev, p1, p2);
                    }
                }
            }
        }
    }
}
