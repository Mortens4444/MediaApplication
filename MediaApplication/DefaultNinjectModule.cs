using DirectShowLib;
using MediaApplication.Filter;
using MediaApplication.General;
using MediaApplication.Media;
using Media.Extensibility.Filter;
using Media.Extensibility.General;
using Media.Extensibility.Media;
using Ninject.Modules;

namespace MediaApplication
{
    public class DefaultNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IFilterPropertyProvider>().To<FilterPropertyProvider>().InSingletonScope();
            Kernel.Bind<IFilterProvider>().To<FilterProvider>().InSingletonScope();
            Kernel.Bind<IFilterRegister>().To<FilterRegister>().InSingletonScope();
            Kernel.Bind<IErrorHandler>().To<ErrorHandler>().InSingletonScope();
            Kernel.Bind<IGeneralGraphBuilder>().To<GeneralGraphBuilder>().InSingletonScope();
            Kernel.Bind<IMediaConnector>().To<MediaConnector>().InSingletonScope();
            Kernel.Bind<IPinProvider>().To<PinProvider>().InSingletonScope();
            Kernel.Bind<ISampleGrabberInitializer>().To<SampleGrabberInitializer>().InSingletonScope();
            Kernel.Bind<IConnectedFilterProvider>().To<ConnectedFilterProvider>().InSingletonScope();
            Kernel.Bind<IConnectionProvider>().To<ConnectionProvider>().InSingletonScope();
            Kernel.Bind<ITypedGraphBuilder>().To<TypedGraphBuilder>().InSingletonScope();
            Kernel.Bind<IMediaEventHandler>().To<MediaEventHandler>().InSingletonScope();
            
            Kernel.Bind<FilterGraph>().ToSelf().InSingletonScope();
        }
    }
}
