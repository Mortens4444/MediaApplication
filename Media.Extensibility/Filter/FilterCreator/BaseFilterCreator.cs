using DirectShowLib;
using System;

namespace Media.Extensibility.Filter.FilterCreator
{
    public class BaseFilterCreator : NamedFilterCreator
    {
        public IBaseFilter BaseFilter { get; private set; }

        public BaseFilterCreator(string name, IBaseFilter baseFilter, Action<IBaseFilter> postAction = null)
            : base(name, postAction)
        {
            BaseFilter = baseFilter;
        }
    }
}
