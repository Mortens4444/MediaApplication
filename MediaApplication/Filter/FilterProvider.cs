using DirectShowLib;
using Media.Extensibility.Filter;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace MediaApplication.Filter
{
    public class FilterProvider : IFilterProvider
    {
        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(int reserved, out IBindCtx pbc);

        private readonly IFilterPropertyProvider filterPropertyProvider;

        public FilterProvider(IFilterPropertyProvider filterPropertyProvider)
        {
            this.filterPropertyProvider = filterPropertyProvider;
        }

        public IBaseFilter CreateFilter(FilterType filterType)
        {
            var guid = filterPropertyProvider.GetGuid(filterType);
            var type = Type.GetTypeFromCLSID(guid);
            return (IBaseFilter)Activator.CreateInstance(type);
        }

        public IBaseFilter CreateFilterByName(string filterName)
        {
            var guid = filterPropertyProvider.GetGuid(filterName);
            return CreateFilterByName(filterName, guid);
        }

        private IBaseFilter CreateFilterByName(string filterName, Guid category)
        {
            var devices = DsDevice.GetDevicesOfCat(category);
            var dev = devices.FirstOrDefault(device => device.Name == filterName);
            if (dev != null)
            {

                IBaseFilter filter = null;
                IBindCtx bindCtx = null;
                try
                {
                    var hr = CreateBindCtx(0, out bindCtx);
                    DsError.ThrowExceptionForHR(hr);
                    var guid = typeof(IBaseFilter).GUID;
                    dev.Mon.BindToObject(bindCtx, null, ref guid, out object obj);
                    filter = (IBaseFilter)obj;
                }
                finally
                {
                    if (bindCtx != null)
                    {
                        Marshal.ReleaseComObject(bindCtx);
                    }
                }

                return filter;
            }
            return null;
        }
    }
}
